using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CRLCP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRLCP.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private CLRCP_MASTERContext _context;
        private TEXTContext _TEXTContext;
        public TextToSpeechContext TextToSpeech;
        private readonly TextToTextContext textContext;
        private readonly IMAGEContext iMAGEContext;
        private readonly ImageToTextContext imageToTextContext;

        public ProcessController(CLRCP_MASTERContext context, 
                                TEXTContext TEXTContext, 
                                TextToSpeechContext textToSpeech, 
                                TextToTextContext textContext, 
                                IMAGEContext iMAGEContext,
                                ImageToTextContext imageToTextContext)
        {
            _context = context;
            _TEXTContext = TEXTContext;
            TextToSpeech = textToSpeech;
            this.textContext = textContext;
            this.iMAGEContext = iMAGEContext;
            this.imageToTextContext = imageToTextContext;
        }

        [HttpPost]
        public async Task<IActionResult> Uploadfile(int UserId,int DataId,int DatasetId,IFormFile file)
        {
            DatasetSubcategoryMapping datasetSubcategoryMapping = _context.DatasetSubcategoryMapping.Find(DatasetId);
            SubCategories destTableName = _context.SubCategories.Find(datasetSubcategoryMapping.DestinationSubcategoryId);

            if (destTableName.Name == "TextSpeech")
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        TextSpeech textSpeech = new TextSpeech();
                        textSpeech.UserId = UserId;
                        textSpeech.Age = _context.UserInfo.Find(UserId).Age;
                        textSpeech.Gender = _context.UserInfo.Find(UserId).Gender;
                        textSpeech.DataId = DataId;
                        textSpeech.LangId = _TEXTContext.Text.Find(DataId).LangId;
                        textSpeech.DomainId = _TEXTContext.Text.Find(DataId).DomainId;
                        textSpeech.OutputData = stream.ToArray();
                        textSpeech.DatasetId = DatasetId;
                        textSpeech.AddedOn = DateTime.Now;
                        var s = TextToSpeech.TextSpeech.ToList();
                        TextToSpeech.TextSpeech.Add(textSpeech);
                        TextToSpeech.SaveChanges();
                        return Ok("true");

                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("false");
                }
            }
            return Ok("false");
            
        }
        
       
        [HttpGet]
        public IEnumerable<Datasets> GetDataset()
        {
            return _context.Datasets.ToList();
        }

        [HttpGet]
        public IActionResult GetText(int DatasetId, int UserId)
        {
            
            int langId = _context.UserInfo.FirstOrDefault(x => x.UserId == UserId).LangId1;

            DatasetSubcategoryMapping datasetSubcategoryMapping = _context.DatasetSubcategoryMapping.Where(x => x.DatasetId == DatasetId).SingleOrDefault();
            if (datasetSubcategoryMapping != null)
            {
                SubCategories sourcetableName = _context.SubCategories.Find(datasetSubcategoryMapping.SourceSubcategoryId);
                SubCategories destTableName = _context.SubCategories.Find(datasetSubcategoryMapping.DestinationSubcategoryId);

                if (sourcetableName.Name == "Text")
                {
                    //List<int> sentences = _TEXTContext.Text.Where(x => x.DatasetId == DatasetId && x.LangId== langId).Select(user => user.DataId).ToList();
                    if (destTableName.Name == "TextSpeech")
                    {
                        List<int> sentences = _TEXTContext.Text.Where(x => x.DatasetId == DatasetId && x.LangId == langId).Select(user => user.DataId).ToList();
                        List<int> UserData = TextToSpeech.TextSpeech.Where(user => user.UserId == UserId).Select(user => user.DataId ).Distinct().ToList();
                        try
                        {
                            List<int> linq = sentences.Except(UserData).ToList();
                            return Ok(new { Text = _TEXTContext.Text.Find(linq.First()).Text1, DataId = linq.First() });
                        }
                        catch (Exception)
                        {
                            return BadRequest();
                        }
                    }
                    else if (destTableName.Name == "TextText")
                    {
                       //langId = 24;//TODO
                        List<int> sentences = _TEXTContext.Text.Where(x => x.DatasetId == DatasetId ).Select(user => user.DataId).ToList();
                        List<int> textText = textContext.TextText.Where(x => x.DatasetId == DatasetId).Select(user => user.DataId).ToList();
                        try
                        {
                            
                            List<int> linq = sentences.Except(textText).ToList();
                            return Ok(new { Text = _TEXTContext.Text.Find(linq.First()).Text1, DataId = linq.First() });
                        }
                        catch (Exception)
                        {
                            return BadRequest();
                        }
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            return BadRequest();

        }

        
        [HttpGet]
        public IActionResult GetImage(int DatasetId, int UserId)
        {
            DatasetSubcategoryMapping datasetSubcategoryMapping = _context.DatasetSubcategoryMapping.Where(x => x.DatasetId == DatasetId).SingleOrDefault();
            if (datasetSubcategoryMapping != null)
            {
                SubCategories sourcetableName = _context.SubCategories.Find(datasetSubcategoryMapping.SourceSubcategoryId);
                SubCategories destTableName = _context.SubCategories.Find(datasetSubcategoryMapping.DestinationSubcategoryId);

                if (sourcetableName.Name == "Images")
                {
                    int langId = _context.UserInfo.FirstOrDefault(x => x.UserId == UserId).LangId1;
                    List<long> Images;
                    if (DatasetId == 2)//TODO
                    {
                        Images = iMAGEContext.Images.Where(x => x.DatasetId == DatasetId).Select(user => user.DataId).ToList();
                    }
                    else
                    {
                        Images = iMAGEContext.Images.Where(x => x.DatasetId == DatasetId && x.LangId == langId).Select(user => user.DataId).ToList();
                    }
                   
                    if (destTableName.Name == "ImageText")
                    {
                        List<long> UserData = imageToTextContext.ImageText.Where(user => user.UserId == UserId).Select(user => user.DataId).Distinct().ToList();
                        try
                        {
                            List<long> linq = Images.Except(UserData).ToList();

                            return Ok(new { ImageString = iMAGEContext.Images.Find(linq.First()).Image, DataId = linq.First() });
                        }
                        catch (Exception)
                        {
                            return BadRequest();
                        }
                    }
                    
                    return BadRequest();
                }
                return BadRequest();
            }
            return BadRequest();
        }


        [HttpPost]
        public IActionResult UploadText(int UserId, int DataId, int DatasetId, string Text,int LangId=0)
        {
            DatasetSubcategoryMapping datasetSubcategoryMapping = _context.DatasetSubcategoryMapping.Where(x => x.DatasetId == DatasetId).SingleOrDefault();
            SubCategories destTableName = _context.SubCategories.Find(datasetSubcategoryMapping.DestinationSubcategoryId);

            if (destTableName.Name == "ImageText")
            {
                try
                {
                    ImageText imageText = new ImageText
                    {
                        UserId = UserId,
                        DataId = DataId,
                        DomainId = iMAGEContext.Images.Where(x=>x.DataId==DataId).FirstOrDefault().DomainId,
                        OutputData = Text,
                        OutputLangId = _context.UserInfo.SingleOrDefault(x => x.UserId == UserId).LangId1,
                        DatasetId = DatasetId,
                        AddedOn = DateTime.Now
                    };
                    imageToTextContext.ImageText.Add(imageText);
                    imageToTextContext.SaveChanges();
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    return BadRequest(false);
                }
            }
            else if (destTableName.Name == "TextText")
            {
                try
                {
                    TextText textText = new TextText
                    {
                        UserId = UserId,
                        DataId = DataId,
                        LangId = _context.UserInfo.SingleOrDefault(x => x.UserId == UserId).LangId1,
                        DomainId = _TEXTContext.Text.FirstOrDefault(x=>x.DataId==DataId).DomainId,
                        OutputData = Text,
                        OutputLangId = _context.UserInfo.SingleOrDefault(x => x.UserId == UserId).LangId1,
                        DatasetId = DatasetId,
                        AddedOn = DateTime.Now
                    };
                    textContext.TextText.Add(textText);
                    textContext.SaveChanges();
                    return Ok(false);
                }
                catch (Exception)
                {
                }
            }
            return Ok(false);
        }

    }
}