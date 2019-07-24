using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRLCP.Dashboard;
using CRLCP.Models;
using CRLCP.Models.DBO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;


namespace CRLCP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private CLRCP_MASTERContext _context;
        private TEXTContext _textContext;
        private JsonResponse jsonResponse;

        public MasterController(CLRCP_MASTERContext context, 
                                TEXTContext textContext, 
                                JsonResponse jsonResponse)
        {
            _context = context;
            _textContext = textContext;
            this.jsonResponse = jsonResponse;
        }
        
        [HttpGet]    //API TO GET HOMEPAGE DASHBOARD COUNTS user wise (ADMIN HOMEPAGE)
        public List<SelectListItem> GetDataSet()
        {
            //List<SelectListItem>
            var _datasets = _context.Datasets;
            List<SelectListItem> _DataSetListItems = _datasets.Select(e => new SelectListItem
            {
                Value = e.DatasetId.ToString(),
                Text = e.Description
            }).ToList();
            //_DataSetListItems.Sort(Function(a, b) a.Text < b.Text)
            return _DataSetListItems;
        }

        [HttpGet]    //API TO GET HOMEPAGE DASHBOARD COUNTS user wise (ADMIN HOMEPAGE)
        public List<SelectListItem> GetLanguageIDs()
        {
            //List<SelectListItem>
            var _languageIds = _context.LanguageIdMapping;
            List<SelectListItem> _languageIdsListItems = _languageIds.Select(e => new SelectListItem
            {
                Value = e.LanguageId.ToString(),
                Text = e.Description
            }).ToList();
            //_DataSetListItems.Sort(Function(a, b) a.Text < b.Text)
            return _languageIdsListItems;
        }

        //source_ID
        //Domain_ID
        [HttpGet]    //API TO GET HOMEPAGE DASHBOARD COUNTS user wise (ADMIN HOMEPAGE)
        public List<SelectListItem> GetSourceIDs()
        {
            //List<SelectListItem>
            var _sourceIds = _context.SourceIdMapping;
            List<SelectListItem> _sourceIdsListItems = _sourceIds.Select(e => new SelectListItem
            {
                Value = e.SourceId.ToString(),
                Text = e.Value
            }).ToList();
            //_DataSetListItems.Sort(Function(a, b) a.Text < b.Text)
            return _sourceIdsListItems;
        }

        [HttpGet]    //API TO GET HOMEPAGE DASHBOARD COUNTS user wise (ADMIN HOMEPAGE)
        public List<SelectListItem> GetDomainIDs()
        {
            //List<SelectListItem>
            var _domainIds = _context.DomainIdMapping;
            List<SelectListItem> _domainIdsListItems = _domainIds.Select(e => new SelectListItem
            {
                Value = e.DomainId.ToString(),
                Text = e.Name
            }).ToList();
            //_DataSetListItems.Sort(Function(a, b) a.Text < b.Text)
            return _domainIdsListItems;
        }

        
        [HttpPost]
        public bool AddText([FromBody]TextModel _textModel)
        {
            bool success = false;
            int _newDomainId = _textModel.DomainId;
            try
            {
                CommonServices _commonServices = new CommonServices();
                if (_textModel.DomainId == 0)
                {
                    _newDomainId=_commonServices.AddDomainIDs(_textModel.NewDomainToAdd.ToString());
                }
                
               

                string[] lines = _textModel.Text1.Split(new[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 0; i < lines.Count(); i++)
                {
                   Text model = new Text();
                    model.DomainId = _newDomainId;
                    model.DatasetId = _textModel.DatasetId;
                    model.AddedOn = _textModel.AddedOn;
                    model.AdditionalInfo = _textModel.AdditionalInfo;
                    model.DataId = _textModel.DataId;
                    model.LangId = _textModel.LangId;
                    model.SourceId = _textModel.SourceId;
                    model.Text1 = lines[i];

                    _textContext.Add(model);
                 
                    success = true;
                }
                _textContext.SaveChanges();
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult SaveLangAccToUserId([FromBody]UsersLanguage model)
        {
            try
            {
                //delete record from UserLanguageMapping table 
                UserLanguageMapping userLanguage = _context.UserLanguageMapping.Find(model.UserId);
                _context.UserLanguageMapping.Remove(userLanguage);
                _context.SaveChangesAsync();

                //insert all value in UserLanguageMapping
                int count = model.languageId.Count();
                for (int i = 0; i < count; i++)
                {
                    _context.UserLanguageMapping.Add(new UserLanguageMapping
                    {
                        UserId = model.UserId,
                        LanguageId = model.languageId[i]

                    });
                }
                _context.SaveChangesAsync();
                jsonResponse.IsSuccessful = true;
                jsonResponse.Response = "Data saved successfully";
                return Ok(jsonResponse);
            }
            catch (Exception)
            {
                jsonResponse.IsSuccessful = true;
                jsonResponse.Response = "Exception Occured";
                return BadRequest(jsonResponse);
            }
        }


        [HttpGet]
        [ActionName("GetLanguage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetLangUserWise(int UserId)
        {
            try
            {
                UsersLanguage usersLanguage = new UsersLanguage();
                List<UserLanguageMapping> list = _context.UserLanguageMapping.Where(e => e.UserId == UserId).ToList();
                usersLanguage.UserId = UserId;
                usersLanguage.languageId = list.Select(x => x.LanguageId).ToList();
                return Ok(usersLanguage);
            }
            catch (Exception)
            {
                return BadRequest(jsonResponse.Response = "Internal Exception");
            }
        }


        [HttpGet]
        [ActionName("GetDomainId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetDomainIdFromText(int DatasetId)
        {
            jsonResponse.Response = "Internal Server Error";
            try
            {
                List<DatasetSubcategoryMapping> lstdatasetSubCatMap = _context.DatasetSubcategoryMapping.ToList();
                int destinationDb_ = lstdatasetSubCatMap.Where(e => e.DatasetId == DatasetId).Select(e => e.SourceSubcategoryId).FirstOrDefault();
                string DestinationDBName = _context.SubCategories.Where(e => e.SubcategoryId == destinationDb_).Select(e => e.Name).FirstOrDefault();
                if (DestinationDBName == "Text")
                {
                    var _list = new HashSet<int>(_textContext.Text.Select(e => e.DomainId));
                    List<DomainIdMapping> _domainIdMapping = _context.DomainIdMapping.ToList();
                    List<DomainIdMapping> _domainIdMappingoutput = _domainIdMapping.Where(e => _list.Contains(e.DomainId)).ToList();
                    return  Ok(_domainIdMappingoutput);
                }
            }
            catch (Exception)
            {
                
                return BadRequest(jsonResponse);
            }
            return BadRequest(jsonResponse);
        }

    }
}