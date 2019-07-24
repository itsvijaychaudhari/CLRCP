using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRLCP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRLCP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly CLRCP_MASTERContext context;
        private readonly TEXTContext _TEXTcontext;
        private readonly TextToSpeechContext textToSpeech;
        private readonly TextToTextContext textContext;
        private readonly IMAGEContext iMAGEContext;
        private readonly ImageToTextContext imageToTextContext;
        private readonly VALIDATION_INFOContext validationInfoContext;
        private readonly JsonResponse jsonResponse;

        public ValidationController(CLRCP_MASTERContext context,
                                TEXTContext TEXTContext,
                                TextToSpeechContext textToSpeech,
                                TextToTextContext textContext,
                                IMAGEContext iMAGEContext,
                                ImageToTextContext imageToTextContext,
                                VALIDATION_INFOContext ValidationInfoContext,
                                JsonResponse jsonResponse)
        {
            this.context = context;
            _TEXTcontext = TEXTContext;
            this.textToSpeech = textToSpeech;
            this.textContext = textContext;
            this.iMAGEContext = iMAGEContext;
            this.imageToTextContext = imageToTextContext;
            validationInfoContext = ValidationInfoContext;
            this.jsonResponse = jsonResponse;
        }

        [HttpGet]
        [ActionName("GetValidationData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetValidationData_TextSpeech(int DatasetId, int UserId, int LanguageId, int DomainId)
        {
            if (DatasetId != 0 && UserId != 0 && LanguageId != 0 && DomainId != 0)
            {
                int? max_collection_user = context.Datasets.Where(x => x.DatasetId == DatasetId)
                                                           .Select(x => x.MaxCollectionUsers)
                                                           .FirstOrDefault();
                DatasetSubcategoryMapping datasetSubcategoryMapping = context.DatasetSubcategoryMapping
                                                            .Where(x => x.DatasetId == DatasetId)
                                                            .SingleOrDefault();

                if (datasetSubcategoryMapping != null)
                {
                    SubCategories sourcetableName = context.SubCategories.Find(datasetSubcategoryMapping.SourceSubcategoryId);
                    SubCategories destTableName = context.SubCategories.Find(datasetSubcategoryMapping.DestinationSubcategoryId);

                    if (destTableName.Name == "TextSpeech")
                    {
                        if (max_collection_user != 0)
                        {
                            List<long> sentences = validationInfoContext.TextspeechValidationResponseDetail.Where(x => x.UserId == UserId).Select(e => e.RefAutoid).ToList();
                            List<long> sentences1 = textToSpeech.TextSpeech.Where(x => x.UserId != UserId && x.IsValid == null
                                               && x.TotalValidationUsersCount < max_collection_user && x.LangId == LanguageId && x.DomainId == DomainId)
                                              .Select(e => e.AutoId).ToList();
                            long id = sentences1.Except(sentences).FirstOrDefault();

                            ValidationTextSpeechModel validationTextSpeechModel = textToSpeech.TextSpeech.Where(x=>x.AutoId == id)
                                               .Select(e => new ValidationTextSpeechModel
                                               {
                                                   DestAutoId = e.AutoId,
                                                   SourceDataId = e.DataId,
                                                   DestinationData = e.OutputData
                                               }).FirstOrDefault();
                            validationTextSpeechModel.SourceData = _TEXTcontext.Text.Where(x => x.DataId == validationTextSpeechModel.SourceDataId).Select(e => e.Text1).FirstOrDefault();
                            validationTextSpeechModel.DatasetID = DatasetId;

                            /*ValidationTextSpeechModel validationTextSpeechModel = textToSpeech.TextSpeech.Where(x => x.UserId != UserId && x.IsValid == null
                                               && x.TotalValidationUsersCount < max_collection_user && x.LangId == LanguageId && x.DomainId == DomainId )
                                               .Select(e => new ValidationTextSpeechModel
                                               {
                                                   DestAutoId = e.AutoId,
                                                   SourceDataId = e.DataId,
                                                   DestinationData = e.OutputData
                                               }).FirstOrDefault();
                            validationTextSpeechModel.SourceData = _TEXTcontext.Text.Where(x => x.DataId == validationTextSpeechModel.SourceDataId).Select(e => e.Text1).FirstOrDefault();
                            validationTextSpeechModel.DatasetID = DatasetId;*/
                            return Ok(validationTextSpeechModel);
                        }
                        return NotFound();

                    }
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [ActionName("SetValidationData")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SetValidationData_TextSpeech(int UserId,int DestAutoId , int DatasetId,int IsMatch,int NoCrossTalk, int IsClear)
        {
            DatasetSubcategoryMappingValidation datasetSubcategoryMappingValidation = context.DatasetSubcategoryMappingValidation
                                                           .Where(x => x.DatasetId == DatasetId)
                                                           .SingleOrDefault();
            if (datasetSubcategoryMappingValidation != null)
            {
                SubCategories destTableNameValidation = context.SubCategories.Find(datasetSubcategoryMappingValidation.DestinationSubcategoryId);

                if (destTableNameValidation.Name == "TEXTSPEECH_VALIDATION_RESPONSE_DETAIL")
                {
                    int IsValidFlag = 0;
                    if (IsMatch == 1 && NoCrossTalk == 1 && IsClear == 1)
                    {
                        IsValidFlag = 1;
                    }
                    validationInfoContext.TextspeechValidationResponseDetail.Add(new TextspeechValidationResponseDetail
                    {
                        UserId = UserId,
                        RefAutoid = DestAutoId,
                        IsMatch = IsMatch,
                        NoCrossTalk = NoCrossTalk,
                        IsClear = IsClear,
                        ValidationFlag = IsValidFlag
                    });

                    

                    ///set count
                    DatasetSubcategoryMapping datasetSubcategoryMapping = context.DatasetSubcategoryMapping
                                                            .Where(x => x.DatasetId == DatasetId)
                                                            .SingleOrDefault();

                    if (datasetSubcategoryMapping != null)
                    {
                        SubCategories destTableName = context.SubCategories.Find(datasetSubcategoryMapping.DestinationSubcategoryId);

                        if (destTableName.Name == "TextSpeech")
                        {
                            TextSpeech textSpeech = textToSpeech.TextSpeech.Where(x => x.AutoId == DestAutoId).Select(x => x).SingleOrDefault();
                            if (textSpeech != null)
                            {
                                textSpeech.TotalValidationUsersCount += 1;
                                if (IsMatch == 1 && NoCrossTalk == 1 && IsClear == 1)
                                {
                                    textSpeech.VoteCount += 1;
                                    
                                }
                                int? maxValidationUsers = context.Datasets.Where(x => x.DatasetId == DatasetId)
                                                           .Select(x => x.MaxValidationUsers)
                                                           .FirstOrDefault();
                                if (maxValidationUsers != null)
                                {
                                    if (maxValidationUsers * 0.5 < textSpeech.VoteCount)
                                    {
                                        textSpeech.IsValid = 1;
                                    }
                                    else if (maxValidationUsers * 0.5 < (textSpeech.TotalValidationUsersCount - textSpeech.VoteCount))
                                    {
                                        textSpeech.IsValid = 0;
                                    }

                                    try
                                    {
                                        validationInfoContext.SaveChangesAsync();
                                        textToSpeech.SaveChangesAsync();
                                    }
                                    catch (Exception)
                                    {
                                        jsonResponse.Response = "Internal Exception";
                                        return BadRequest(jsonResponse);
                                    }
                                    jsonResponse.IsSuccessful = true;
                                    jsonResponse.Response = "Saved";
                                    return Ok(jsonResponse);
                                }
                            }
                        }
                    }
                }
            }
            jsonResponse.IsSuccessful = false;
            jsonResponse.Response = "Details not match";
            return NotFound(jsonResponse);
        }
    }
}