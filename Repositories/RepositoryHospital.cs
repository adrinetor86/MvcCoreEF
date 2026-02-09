using MvcCoreEF.Data;
using MvcCoreEF.Models;

namespace MvcCoreEF.Repositories
{
    public class RepositoryHospital
    {

        private HospitalContext _context;

        public RepositoryHospital(HospitalContext context)
        {
            _context = context;
        }

        public List<Hospital> GetHospitales()
        {
            var consulta = from datos in _context.Hospitales
                           select datos;

            return consulta.ToList();
        }
    }
}
