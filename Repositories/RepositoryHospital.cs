using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Hospital>> GetHospitalesAync()
        {
            var consulta = from datos in _context.Hospitales
                           select datos;

            return await consulta.ToListAsync();
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            var consulta = from datos in _context.Hospitales
                where datos.IdHospital == idHospital
                select datos;
            
           //CUANDO BUSCAMOS, SI NO ENCUENTRA ALGO DEBEMOS DEVOLVER NULL

           return await consulta.FirstOrDefaultAsync();

        }


        public async Task CreateHospitalAsync(int idHospital,string nombre,string direccion, string telefono,int camas)
        {
            //CREAMOS UN NUEVO MODEL

            Hospital hospital = new Hospital();
            //ASIGNAMOS SUS PROPIEDADES
            hospital.IdHospital = idHospital;
            hospital.Nombre = nombre;
            hospital.Direccion = direccion;
            hospital.Telefono = telefono;
            hospital.Camas = camas;
            //AÑADIMOS NUESTRO OBJETO AL DBSET
            //AHORA MISMO ES TEMPORAL, ESTA EN LA COLECCION
            //SALDRA EN LAS CONSULTAS, PERO NO ESTA EN LA BBDD
            await _context.Hospitales.AddAsync(hospital);
            
            //GUARDAMOS EN LA BASE DE DATOS
            await _context.SaveChangesAsync();
        }


        public async Task DeleteHospitalAsync(int idHospital)
        {
            Hospital hospital = await FindHospitalAsync(idHospital);
            _context.Hospitales.Remove(hospital);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateHospitalAsync(int idHospital,string nombre,string direccion,string telefono,int camas)
        {
            Hospital hospital = await FindHospitalAsync(idHospital);

            hospital.Nombre = nombre;
            hospital.Direccion = direccion;
            hospital.Telefono = telefono;
            hospital.Camas = camas;

            await _context.SaveChangesAsync();
        }
        
    }
}
