using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Testproject.Data;
using Testproject.Model;
using Microsoft.EntityFrameworkCore;
using Testproject.Models;
using AutoMapper;

namespace Testproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly DataContext dbContext;

        public DetailsController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [Route("personDetails")]
        public async Task<IActionResult> personDetails()
        {
            var details = await dbContext.Detail.ToListAsync();

            return Ok(details);
        }

        [HttpGet]
        [Route("getDetailByID/{id:int}")]
        [ActionName("getDetailByID")]
        public async Task<IActionResult> getDetailByID(int id)
        {
            var details = await dbContext.Detail.FirstOrDefaultAsync(x => x.Id == id);
            var addressDetails=await dbContext.Address.FirstOrDefaultAsync(x=>x.Id==id);

  

            if (addressDetails != null)
            {
                return Ok(addressDetails);
            }
            return NotFound();
        }
        //[HttpPost]
        //public async Task<IActionResult> postDetail(AddDetails addDetails)
        //{
        //    Var detail=new Details()
        //}

        [HttpPost]
        [Route("updateDetail/{id:int}")]
        public async Task<IActionResult> updateDetail([FromRoute] int id,AddDetails addDetails)
        {
            var details = new Details()
            {
                lname = addDetails.lname,
                fname = addDetails.fname,
                Tel = addDetails.Tel,
            };
            var address = new Address()
            {
                addr = addDetails.addr,
                St = addDetails.St,
                city = addDetails.city,
                addrtype = addDetails.addrtype,
                zip = addDetails.zip,
            };

            var existId = await dbContext.Detail.FindAsync(id);

                if(existId!=null)
            {
                existId.lname = addDetails.lname;
                existId.fname = addDetails.fname;
                existId.Tel = addDetails.Tel;
            }
            var exId = await dbContext.Address.FindAsync(id);

            if (exId != null)
            {
                exId.addrtype = addDetails.addrtype;
                exId.zip = addDetails.zip;
                exId.St= addDetails.St;
                exId.city = addDetails.city;    
                exId.addr = addDetails.addr;
               
                await dbContext.SaveChangesAsync();

                return Ok(addDetails);

            }
            return NotFound();
        }
        

    }
}
