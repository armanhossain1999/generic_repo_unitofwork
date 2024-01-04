
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Masters_Details_CRUD.Models;
using Masters_Details_CRUD.ViewModels;
using Masters_Details_CRUD.Repositories.interfaces;
using X.PagedList;

namespace Masters_Details_CRUD.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepo<Applicant> repo;
    
        public ApplicantsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepo<Applicant>();
        } 
        public async Task <ActionResult> Index(int pg = 1)
        {
            var data = await this.repo.GetAllAsync(x => x.Include(y => y.Qualifications));
            var pagedData = await data.OrderBy(x => x.ApplicantId).ToPagedListAsync(pg, 5);
            return View(pagedData);
        }
        public ActionResult Create()
        {
            ApplicantInputModel data = new ApplicantInputModel(); //var data = new ApplicantInputModel();
            data.Qualifications.Add(new Qualification { });
            return View(data);
        } 
        [HttpPost]
        public async Task <IActionResult> Create(ApplicantInputModel model, string act = "")
        {
            if (act == "add")
            {
                model.Qualifications.Add(new Qualification { });
            }
            if (act.StartsWith("remove"))
            {
                var index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Qualifications.RemoveAt(index);

                foreach (var v in ModelState.Values)
                {
                    v.RawValue = null;
                    v.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    Applicant a = new Applicant //var a = new Applicant
                    {
                        ApplicantName = model.ApplicantName,
                        BirthDate = model.BirthDate,
                        Gender = model.Gender,
                        AppliedFor = model.AppliedFor,
                        IsReadyToWork = model.IsReadyToWork
                    };
                    foreach (var l in model.Qualifications)
                    {

                        a.Qualifications.Add(l);

                        //or
                        //a.Qualifications.Add(new Qualification
                        //{
                        //    Degree=l.Degree,
                        //    Institute=l.Institute,
                        //    PassingYear=l.PassingYear,
                        //    Result=l.Result
                        //});
                    }
                    await repo.InsertAsync(a);
                    bool saved = await unitOfWork.SaveAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var a = await repo.GetByIdAsync(x => x.ApplicantId == id, x => x.Include(y => y.Qualifications));

            if (a == null)
            {
                return NotFound();
            }

            var model = new ApplicantEditModel
            {
                ApplicantId = a.ApplicantId,
                ApplicantName = a.ApplicantName,
                AppliedFor = a.AppliedFor,
                BirthDate = a.BirthDate,
                Gender = a.Gender,
                IsReadyToWork = a.IsReadyToWork
            };

            foreach (var t in a.Qualifications)
            {
                model.Qualifications.Add(t);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicantEditModel model, string act = "")
        {
            if (act == "add")
            {
                model.Qualifications.Add(new Qualification());
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Qualifications.RemoveAt(index);

                // Clear errors and reset values for removed qualifications
                foreach (var v in ModelState.Values)
                {
                    v.RawValue = null;
                    v.Errors.Clear();
                }
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var applicantRepo = unitOfWork.GetRepo<Applicant>();

                    var applicant = await applicantRepo.GetByIdAsync(x => x.ApplicantId == model.ApplicantId, x => x.Include(y => y.Qualifications));

                    if (applicant == null)
                    {
                        return NotFound();
                    }

                    applicant.ApplicantName = model.ApplicantName;
                    applicant.AppliedFor = model.AppliedFor;
                    applicant.BirthDate = model.BirthDate;
                    applicant.Gender = model.Gender;

                    // Delete existing qualifications using the DeleteAsync method
                    await this.repo.DeleteAsync(q => q.ApplicantId == model.ApplicantId);

                    // Add new qualifications
                    applicant.Qualifications.Clear();
                    foreach (var x in model.Qualifications)
                    {
                        applicant.Qualifications.Add(new Qualification { Degree = x.Degree, Institute = x.Institute, PassingYear = x.PassingYear, Result = x.Result });
                    }

                    await applicantRepo.UpdateAsync(applicant);
                    bool saved = await unitOfWork.SaveAsync();
                    return RedirectToAction("Index");

                }
            }

            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await this.repo.GetByIdAsync(x => x.ApplicantId == id);
            if (data == null) return NotFound();
            await this.repo.DeleteAsync(x => x.ApplicantId == id);
            await this.unitOfWork.SaveAsync();
            return RedirectToAction("Index");
            //return Json(new { success = true, id });
        }
    }
}


















//public async Task<IActionResult> Delete(int id)
//{
//    var applicantRepo = unitOfWork.GetRepo<Applicant>();        
//    await applicantRepo.DeleteAsync(x => x.ApplicantId == id);
//    bool saved = await unitOfWork.SaveAsync();
//    return RedirectToAction("Index");
//}


