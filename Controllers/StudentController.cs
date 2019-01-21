
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APINetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APINetCore.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private StudentDBContext _context = null;
        public StudentController(StudentDBContext context)
        {
            this._context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblStudent>>> Get()
        {
            List<TblStudent> lststudent = null;
            try
            {
                using (_context)
                {
                    lststudent = await _context.TblStudent.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return BadRequest();
            }
            return Ok(lststudent);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblStudent>> Get(int id)
        {
            TblStudent student = null;
            try
            {
                using (_context)
                {
                    student = await _context.TblStudent.FirstOrDefaultAsync(x => x.StudentId == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return BadRequest();
            }
            return Ok(student);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<TblStudent>> Post([FromBody]TblStudent student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            using (_context)
            {
                using (var _ctxTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _context.TblStudent.Add(student);
                        await _context.SaveChangesAsync();

                        _ctxTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _ctxTransaction.Rollback();
                        ex.ToString();
                        return BadRequest();
                    }
                }
            }
            return Ok(student);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TblStudent>> Put(int id, [FromBody]TblStudent student)
        {
            if (student == null || id <= 0)
            {
                return BadRequest();
            }
            using (_context)
            {
                using (var _ctxTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var entityUpdate = _context.TblStudent.FirstOrDefault(x => x.StudentId == id);
                        if (entityUpdate != null)
                        {
                            entityUpdate.FirstName = student.FirstName;
                            entityUpdate.LastName = student.LastName;
                            entityUpdate.Address = student.Address;
                            entityUpdate.Email = student.Email;
                            entityUpdate.Password = student.Password;
                            entityUpdate.Status = student.Status;
                            await _context.SaveChangesAsync();
                        }
                        else
                            return NotFound();
                        await _context.SaveChangesAsync();
                        _ctxTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _ctxTransaction.Rollback();
                        ex.ToString();
                        return BadRequest();
                    }
                }
            }
            return Ok(student);

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (_context)
            {
                using (var _ctxTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var idToRemove = _context.TblStudent.SingleOrDefault(x => x.StudentId == id);
                        if (idToRemove != null)
                        {
                            _context.TblStudent.Remove(idToRemove);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return NotFound();
                        }
                        _ctxTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback(); e.ToString();
                        return BadRequest();
                    }
                }
            }
            return Ok();
        }
    }
}
