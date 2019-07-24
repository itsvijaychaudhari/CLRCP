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
        private readonly JsonResponse jsonResponse;

        public ProcessController(CLRCP_MASTERContext context, 
                                TEXTContext TEXTContext, 
                                TextToSpeechContext textToSpeech, 
                                TextToTextContext textContext, 
                                IMAGEContext iMAGEContext,
                                ImageToTextContext imageToTextContext,
                                JsonResponse jsonResponse)
        {
            _context = context;
            _TEXTContext = TEXTContext;
            TextToSpeech = textToSpeech;
            this.textContext = textContext;
            this.iMAGEContext = iMAGEContext;
            this.imageToTextContext = imageToTextContext;
            this.jsonResponse = jsonResponse;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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
                        jsonResponse.IsSuccessful = true;
                        jsonResponse.Response = "File Uploaded Successfully";
                        return Ok(jsonResponse);

                    }
                }
                catch (Exception ex)
                {
                    jsonResponse.IsSuccessful = false;
                    jsonResponse.Response = "Internal Exception";
                    return BadRequest(jsonResponse);
                }
            }
            jsonResponse.IsSuccessful = false;
            jsonResponse.Response = "Table Not Found.";
            return NotFound(jsonResponse);
        }
        
       
        [HttpGet]
        public IEnumerable<Datasets> GetDataset()
        {
            return _context.Datasets.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetText(int DatasetId, int UserId, int langId, int DomainId)
        {
            
            //int langId = _context.UserInfo.FirstOrDefault(x => x.UserId == UserId).LangId1;

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
                        List<int> sentences = _TEXTContext.Text.Where(x => x.DatasetId == DatasetId && x.LangId == langId && x.DomainId == DomainId).Select(user => user.DataId).ToList();
                        List<int> UserData = TextToSpeech.TextSpeech.Where(user => user.UserId == UserId).Select(user => user.DataId ).Distinct().ToList();
                        List<int> linq = sentences.Except(UserData).ToList();
                        return Ok(new
                        {
                            Text = _TEXTContext.Text.Find(linq.First()).Text1,
                            DataId = linq.First()
                        });
                    }
                    else if (destTableName.Name == "TextText")
                    {
                       //langId = 24;//TODO
                        List<int> sentences = _TEXTContext.Text.Where(x => x.DatasetId == DatasetId ).Select(user => user.DataId).ToList();
                        List<int> textText = textContext.TextText.Where(x => x.DatasetId == DatasetId).Select(user => user.DataId).ToList();
                        List<int> linq = sentences.Except(textText).ToList();
                        return Ok(new
                        {
                            Text = _TEXTContext.Text.Find(linq.First()).Text1,
                            DataId = linq.First()
                        });
                    }
                }
            }
            return NotFound();
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
                        List<long> linq = Images.Except(UserData).ToList();
                        return Ok(new { ImageString = iMAGEContext.Images.Find(linq.First()).Image, DataId = linq.First() });
                    }
                }
            }
            return NotFound();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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
                    jsonResponse.IsSuccessful = true;
                    jsonResponse.Response = "Text aved";
                    return Ok(jsonResponse);
                }
                catch (Exception)
                {
                    jsonResponse.IsSuccessful = false;
                    jsonResponse.Response = "Text not saved";
                    return BadRequest(jsonResponse);
                }
            }
            jsonResponse.IsSuccessful = false;
            jsonResponse.Response = "Text not saved";
            return BadRequest(jsonResponse);
        }

    }
}