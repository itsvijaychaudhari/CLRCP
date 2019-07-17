using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRLCP.Helper;
using CRLCP.Models;
using CRLCP.Models.Dashboard;
using CRLCP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CRLCP.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
       
        
        private CLRCP_MASTERContext _context;
        private TEXTContext _textContext;
        private TextToSpeechContext _textToSpeechContext;
        private IMAGEContext _imageContext;
       private ImageToTextContext _imageToTextContext;
       private TextToTextContext _textToTextContext;

       
       

        public DashboardController(CLRCP_MASTERContext context,
            TEXTContext textContext, TextToSpeechContext textToSpeechContext, 
            IMAGEContext imageContext, ImageToTextContext imageToTextContext, 
            TextToTextContext textToTextContext, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _textContext = textContext;
            _textToSpeechContext = textToSpeechContext;
            
            _imageContext = imageContext;
            _imageToTextContext = imageToTextContext;
            _textToTextContext = textToTextContext;
        }

        

        //POST api/Dashboard
        [HttpPost]    //API TO GET HOMEPAGE DASHBOARD COUNTS (ADMIN HOMEPAGE)
        public List<HomePageModel> GetHomePage([FromBody] HomePageRequestModel loginData)
        {
            List<HomePageModel> GeneralHomePageDataList = new List<HomePageModel>();
            try
            {
                List<DatasetSubcategoryMapping> model = new List<DatasetSubcategoryMapping>();
                model = _context.DatasetSubcategoryMapping.ToList();
                int sourceDbCount = 0;
                int destinationDbCount = 0;
                //foreach to fetch overall source and destination counts per dataset
                //for eg: ASR     source count:2  destination count: 15
                foreach (var item in model)
                {
                    
                    HomePageModel homePageCount = new HomePageModel();
                    destinationDbCount = 0;
                    sourceDbCount = 0;
                    string DatasetName = Convert.ToString(_context.Datasets.Where(e => e.DatasetId == item.DatasetId).Select(e => e.Name).FirstOrDefault()) ?? "";

                    string SourceDB = Convert.ToString(_context.SubCategories.Where(e => e.SubcategoryId == item.SourceSubcategoryId).Select(e => e.Name).FirstOrDefault()) ?? "";

                    string DestinationDB = Convert.ToString(_context.SubCategories.Where(e => e.SubcategoryId == item.DestinationSubcategoryId).Select(e => e.Name).FirstOrDefault()) ?? "";

                    if(SourceDB == "Text")
                    {
                         sourceDbCount = _textContext.Text.Where(e => e.DatasetId == item.DatasetId).Count();
                        if (DestinationDB == "TextSpeech")
                        {
                            destinationDbCount = _textToSpeechContext.TextSpeech.Where(e => e.DatasetId == item.DatasetId).Count();
                        }

                        else if (DestinationDB == "TextText")
                        {
                            destinationDbCount = _textToTextContext.TextText.Where(e => e.DatasetId == item.DatasetId).Count();
                        }
                    }

                    if (SourceDB == "Images")
                    {
                        sourceDbCount = _imageContext.Images.Where(e => e.DatasetId == item.DatasetId).Count();
                        if (DestinationDB == "ImageText")
                        {
                            destinationDbCount = _imageToTextContext.ImageText.Where(e => e.DatasetId == item.DatasetId).Count();
                        }

                    }
                    homePageCount.DataSetName = DatasetName;
                    homePageCount.SourceDataCount = sourceDbCount;
                    homePageCount.CollectedDataCount = destinationDbCount;
                    GeneralHomePageDataList.Add(homePageCount);
                }
                return GeneralHomePageDataList;
            }
            catch (Exception ex)
            {
                return null;
                //return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost]    //API TO GET HOMEPAGE DASHBOARD COUNTS user wise (ADMIN HOMEPAGE)
        public List<UserWiseDataCountModel> GetHomePageCountUserWise([FromBody] HomePageRequestModel loginData)
        {
            //list of userwise data model
            List<UserWiseDataCountModel> UserWiseDataList = new List<UserWiseDataCountModel>();

            List<HomePageUserWiseCount> HomePageUserwiseDataList = new List<HomePageUserWiseCount>();
            try
            {
                

                //list of all datasets
                List<Datasets> Datasets = new List<Datasets>();
                Datasets = _context.Datasets.ToList();

                //list of dataset and destination
                List<DatasetSubcategoryMapping> model = new List<DatasetSubcategoryMapping>();
                model = _context.DatasetSubcategoryMapping.ToList();

                foreach (var item in Datasets)
                {
                    int destinationDb_ = Convert.ToInt32(model.Where(e => e.DatasetId == item.DatasetId).Select(e=>e.DestinationSubcategoryId).FirstOrDefault());

                    string DestinationDBName = Convert.ToString(_context.SubCategories.Where(e => e.SubcategoryId == destinationDb_).Select(e => e.Name).FirstOrDefault()) ?? "";

                    if (DestinationDBName == "TextSpeech")
                    {
                        var firstCount = _textToSpeechContext.TextSpeech.Where(e => e.DatasetId == item.DatasetId).GroupBy(e => e.UserId).Select(e => new UserWiseDataCountModel
                        {
                            UserId = e.Key,
                            Datasetcount = e.Count(),
                            UserName = "",
                            DataSetName = "",
                            DatasetId = item.DatasetId,
                        }).ToList();

                        var userInfo = _context.UserInfo;

                        var secondCount = firstCount.Join(userInfo, c => c.UserId, u => u.UserId, (c, u) => new UserWiseDataCountModel
                        {
                            UserId = u.UserId,
                            UserName = u.Name,
                            Datasetcount = c.Datasetcount,
                            DatasetId = c.DatasetId,
                            DataSetName = "",

                        });
                        var thirdCount = Datasets.Join(secondCount, d => d.DatasetId, s => s.DatasetId, (d, s) => new UserWiseDataCountModel
                        {

                            UserId = s.UserId,
                            UserName = s.UserName,
                            Datasetcount = s.Datasetcount,
                            DatasetId = s.DatasetId,
                            DataSetName = d.Name,

                        });


                        foreach (var data in thirdCount)
                        {
                            UserWiseDataList.Add(data);
                        }
                    }
                }

            }


           

                //return GeneralHomePageDataList;
            //return null;
          
            catch (Exception ex)
            {
                return null;
                //return BadRequest(new { message = ex.Message });
            }
            return UserWiseDataList;
        }

    }
}