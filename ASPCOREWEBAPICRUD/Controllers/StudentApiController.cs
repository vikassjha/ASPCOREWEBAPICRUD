using ASPCOREWEBAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCOREWEBAPICRUD;

namespace ASPCOREWEBAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly MyDBContext context
            ;
        public StudentApiController(MyDBContext context
            )
        {
            this.context = context;
        }
        [HttpGet]

        public async Task<ActionResult<List<Student>>> getStudent()
        {
            var data = await context.Students.ToListAsync();
            return data;
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> getStudentById(int id)
        {




            var stds = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            return stds;
        }

        [HttpPost]

        public async Task<ActionResult<Student>> insertStudent(Student student)
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return Ok(student);
        }
        [HttpPut("{id}")]

        public async Task<ActionResult> updateStudent(int id,Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State= EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> deleteStudent(int id)
        {
            var std= await context.Students.FirstOrDefaultAsync(x=>x.Id==id);
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
    }
}
