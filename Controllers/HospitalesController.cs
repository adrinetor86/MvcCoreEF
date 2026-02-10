using Microsoft.AspNetCore.Mvc;
using MvcCoreEF.Models;
using MvcCoreEF.Repositories;

namespace MvcCoreEF.Controllers;

public class HospitalesController : Controller
{
    private RepositoryHospital _repo;
    
    
    public HospitalesController(RepositoryHospital repo)
    {
        _repo = repo;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        List<Hospital> hospitales = await _repo.GetHospitalesAync();
        
        return View(hospitales);
    }

    public async Task<IActionResult> Details(int idHospital)
    {
        Hospital hospital = await _repo.FindHospitalAsync(idHospital);
        return View(hospital);
    }


    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Hospital hospital)
    {
        await _repo.CreateHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono,
            hospital.Camas);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int idHospital)
    {
        await _repo.DeleteHospitalAsync(idHospital);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int idHospital)
    {
        Hospital hospital = await _repo.FindHospitalAsync(idHospital);
        return View(hospital);
    }
    [HttpPost]
    public async Task<IActionResult> Update(Hospital hospital)
    {
        await _repo.UpdateHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono,
            hospital.Camas);
        return RedirectToAction("Index");
    }
}