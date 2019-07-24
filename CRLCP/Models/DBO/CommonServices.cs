using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Models.DBO
{
    public class CommonServices
    {
        private CLRCP_MASTERContext _context;
        public CommonServices(CLRCP_MASTERContext context)
        {
            _context = context;
        }
        public CommonServices()
        {

        }
        public int  AddDomainIDs(string _domainValue)
        {
            DomainIdMapping _domainIdMapping = new DomainIdMapping();
            _domainIdMapping.Name = _domainValue;
            bool success = false;
            int _domainId=0;
            try
            {
                _context.Add<DomainIdMapping>(_domainIdMapping);
                //_context.DomainIdMapping.Add(_domainIdMapping);
                _context.SaveChanges();
                 _domainId = Convert.ToInt32( _context.DomainIdMapping.Where(e => e.Name == _domainValue).Select(e=>e.DomainId).FirstOrDefault());
                success = true;
            }
            catch(Exception ex)
            {
                success = false;
            }
            return _domainId;
        }
    }
}
