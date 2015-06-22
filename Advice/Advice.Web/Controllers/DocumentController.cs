using System;
using System.Linq;
using System.Web.Http;
using Advice.Application.Contracts;
using Advice.Infrastructure.Security;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/documents")]
    public class DocumentController : ApiController
    {
        private readonly IDocumentService _documentService;
        private readonly IImpersonator _impersonator;

        public DocumentController(IDocumentService documentService, IImpersonator impersonator)
        {
            _documentService = documentService;
            _impersonator = impersonator;
        }

        [Route("{id:int}/{filename}")]
        public void GetDocumentByDocumentId(int id, string filename)
        {
            try
            {                   
                var documentPath = _documentService.GetFullDocumentPathByDocumentId(id);
                
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(documentPath);
                // CHANGE THIS TO GET VALUES FROM CONFIG FILE OR ELSEWHERE
                _impersonator.ImpersonateValidUser("advice.services", "HQ", "1mp3rs0nat3!");

                var response = System.Web.HttpContext.Current.Response;
                
                response.Clear();
                response.ClearHeaders();
                response.ClearContent();

                response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                response.AddHeader("Content-Length", fileInfo.Length.ToString());
                response.ContentType = "application/octet-string";
                
                response.TransmitFile(documentPath);
             
                response.End();	
            }
            finally
            {
                _impersonator.Dispose();
            }
        }
    }
}
