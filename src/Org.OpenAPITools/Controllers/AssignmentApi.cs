/*
 * Biletado services
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * Contact: dh@blaimi.de
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Org.OpenAPITools.Attributes;
using Org.OpenAPITools.Models;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Net;
using System.Linq;
using static Org.OpenAPITools.Models.Assignment;
using Npgsql;
using System.Data;

namespace Org.OpenAPITools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class AssignmentApiController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public AssignmentApiController(DatabaseContext dbContext)
        {
            databaseContext = dbContext;
        }
        /// <summary>
        /// get all personal assignments
        /// </summary>
        /// <param name="employeeId">filter for a given employee</param>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/personal/assignments/")]
        [ValidateModelState]
        [SwaggerOperation("PersonalAssignmentsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(PersonalAssignmentsGet200Response), description: "successful operation")]
        public virtual IActionResult PersonalAssignmentsGet([FromQuery(Name = "employee_id")] Guid? employeeId)
        {
            if (employeeId == null)
            {
                var assignments = databaseContext.assignments;
                return new JsonResult(assignments);
            }
            else
            {
                List<Assignment> matchingempl = databaseContext.assignments.Where(a => a.employee_id == employeeId)
                    .ToList();
                return new JsonResult(matchingempl);
            }
        }

        /// <summary>
        /// delete an assignment by id
        /// </summary>
        /// <param name="id">uuid of the assignment</param>
        /// <response code="204">successful operation</response>
        /// <response code="401">if no (valid) authentication is given</response>
        /// <response code="404">not found</response>
        [HttpDelete]
        [Route("/personal/assignments/{id}/")]
        [ValidateModelState]
        [SwaggerOperation("PersonalAssignmentsIdDelete")]
        [SwaggerResponse(statusCode: 401, type: typeof(Error), description: "if no (valid) authentication is given")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "not found")]
        public virtual IActionResult PersonalAssignmentsIdDelete([FromRoute(Name = "id")][Required] Guid id)
        {
            var assignment = databaseContext.assignments.Find(id);
            if (assignment != null)
            {
                databaseContext.Remove(assignment);
                databaseContext.SaveChanges();
                return StatusCode(204);
            }
            else
            {
                return StatusCode(404);
            }
        }

        /// <summary>
        /// get an assignment by id
        /// </summary>
        /// <param name="id">uuid of the assignment</param>
        /// <response code="200">successful operation</response>
        /// <response code="404">not found</response>
        [HttpGet]
        [Route("/personal/assignments/{id}/")]
        [ValidateModelState]
        [SwaggerOperation("PersonalAssignmentsIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Assignment), description: "successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "not found")]
        public virtual IActionResult PersonalAssignmentsIdGet([FromRoute(Name = "id")][Required] Guid id)
        {
            var assignment = databaseContext.assignments.Find(id);
            if (assignment!= null)
            {
                return StatusCode(200, new JsonResult(assignment));
            }
            else return StatusCode(404, default(Error));
        }

        /// <summary>
        /// add or update an assignment by id
        /// </summary>
        /// <remarks>if an id is supplied in the object, it MUST mach with the one in the url</remarks>
        /// <param name="id">uuid of the assignment</param>
        /// <param name="assignment"></param>
        /// <response code="204">successful operation</response>
        /// <response code="400">invalid input</response>
        /// <response code="401">if no (valid) authentication is given</response>
        /// <response code="422">if the reservation already has an assignment with the given role or the employee does not exist or the reservation does not exist or mismatching id in url and object </response>
        [HttpPut]
        [Route("/personal/assignments/{id}/")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("PersonalAssignmentsIdPut")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "invalid input")]
        [SwaggerResponse(statusCode: 401, type: typeof(Error), description: "if no (valid) authentication is given")]
        [SwaggerResponse(statusCode: 422, type: typeof(Error), description: "if the reservation already has an assignment with the given role or the employee does not exist or the reservation does not exist or mismatching id in url and object ")]
        public virtual IActionResult PersonalAssignmentsIdPut([FromRoute(Name = "id")][Required] Guid id, [FromBody] Assignment assignment)
        {
            if (assignment.id == id)
            {
                if ((assignment.role!=Assignment.assignment_role.cleanup) &(assignment.role !=Assignment.assignment_role.service))
                {
                    //keine richtige rolle
                    return StatusCode(422, "NO valid Role");
                }

                string employeeUri = Environment.GetEnvironmentVariable("PERSONAL_EMPLOYEE_URI") ?? "http://localhost:9000/personal/employees/";
                string url = employeeUri + assignment.employee_id;
                if (Helpers.ApiRequest.HTTPreq(url).Result)
                {
                    //Employee gefunden
                }
                else { return StatusCode(422, "Employee not found"); }

                string reservationsUri = Environment.GetEnvironmentVariable("PERSONAL_RESERVATIONS_URI") ?? "http://localhost/api/reservations/";
                url = reservationsUri + assignment.reservation_id+"/";
                if (Helpers.ApiRequest.HTTPreq(url).Result)
                {
                    //reservation gefunden
                }
                else { return StatusCode(422, "Reservation not found"); }


                // reservation hat assignment mit gleicher role?
                List<Assignment> existingAssignmentReservations = databaseContext.assignments.Where(a => a.reservation_id == assignment.reservation_id)
                    .ToList();

                if (existingAssignmentReservations.Count > 0)
                {
                    foreach (Assignment existingAssignmentReservation in existingAssignmentReservations)
                    {
                        if (existingAssignmentReservation.role == assignment.role)
                        {
                            return StatusCode(422, "Reservation already has assignement with same role");
                        }
                    }
                }

                //Keine id �bergeben
                if (assignment.id == Guid.Empty)
                {
                    //No id is given in request body So it creates assig id 
                    assignment.id = Guid.NewGuid();

                }

                //gibt es das assignment mit der id schon?
                var exis_ass = databaseContext.assignments.Find(assignment.id);
                if (exis_ass != null)//Gibt es schon UPDATE
                {
                    exis_ass.employee_id = assignment.employee_id;
                    exis_ass.reservation_id = assignment.reservation_id;
                    exis_ass.role = assignment.role;
                    databaseContext.Update(exis_ass);
                    databaseContext.SaveChanges();
                    return StatusCode(204);
                }
                else //id ist unbekannt oder wurde nicht gefunden CREATE
                {
                    databaseContext.assignments.Add(assignment);
                    databaseContext.SaveChanges();
                    return StatusCode(204);
                }
            }
            else return StatusCode(422, "Mismatch in ID and Object");
        }


        /// <summary>
        /// add a new assignment
        /// </summary>
        /// <remarks>MAY contain a uuid. If so, this method does the same checks as &#x60;PUT&#x60; does.</remarks>
        /// <param name="assignment"></param>
        /// <response code="200">Successful operation of updating an existing assignment. This can only happen if a uuid gets passed. </response>
        /// <response code="201">successful operation of creating a new assignment</response>
        /// <response code="400">invalid input</response>
        /// <response code="401">if no (valid) authentication is given</response>
        /// <response code="422">if the reservation already has an assignment with the given role or the employee does not exist or the reservation does not exist </response>
        [HttpPost]
        [Route("/personal/assignments/")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("PersonalAssignmentsPost")]
        [SwaggerResponse(statusCode: 200, type: typeof(Assignment), description: "Successful operation of updating an existing assignment. This can only happen if a uuid gets passed. ")]
        [SwaggerResponse(statusCode: 201, type: typeof(Assignment), description: "successful operation of creating a new assignment")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "invalid input")]
        [SwaggerResponse(statusCode: 401, type: typeof(Error), description: "if no (valid) authentication is given")]
        [SwaggerResponse(statusCode: 422, type: typeof(Error), description: "if the reservation already has an assignment with the given role or the employee does not exist or the reservation does not exist ")]
        public virtual IActionResult PersonalAssignmentsPost([FromBody] Assignment assignment)
        {
            if ((assignment.role!=Assignment.assignment_role.cleanup) &(assignment.role !=Assignment.assignment_role.service))
            {
                //keine richtige rolle
                return StatusCode(422, "NO valid Role");
            }

            string employeeUri = Environment.GetEnvironmentVariable("PERSONAL_EMPLOYEE_URI") ?? "http://localhost:9000/personal/employees/";
            string url = employeeUri + assignment.employee_id;
            if (Helpers.ApiRequest.HTTPreq(url).Result)
            {
                //Employee gefunden
            }
            else { return StatusCode(422, "Employee not found"); }

            string reservationsUri = Environment.GetEnvironmentVariable("PERSONAL_RESERVATIONS_URI") ?? "http://localhost/api/reservations/";
            url = reservationsUri + assignment.reservation_id+"/";
            if (Helpers.ApiRequest.HTTPreq(url).Result)
            {
                //reservation gefunden
            }
            else { return StatusCode(422, "Reservation not found"); }

            //reservation hat assignment mit gleicher role?
            List<Assignment> existingAssignmentReservations = databaseContext.assignments.Where(a => a.reservation_id == assignment.reservation_id)
            .ToList();

            if (existingAssignmentReservations.Count > 0)
            {
                foreach (Assignment existingAssignmentReservation in existingAssignmentReservations)
                {
                    if (existingAssignmentReservation.role == assignment.role)
                    {
                        return StatusCode(422, "Reservation already has assignement with same role");
                    }
                }
            }

            //Keine id �bergeben
            if (assignment.id == Guid.Empty)
            {
                //No id is given in request body So it creates assig id 
                assignment.id = Guid.NewGuid();
            }

            //gibt es das assignment mit der id schon?
            var exis_ass = databaseContext.assignments.Find(assignment.id);
            if (exis_ass != null)//Gibt es schon UPDATE
            {
                exis_ass.employee_id = assignment.employee_id;
                exis_ass.reservation_id = assignment.reservation_id;
                exis_ass.role = assignment.role;
                databaseContext.Update(exis_ass);
                databaseContext.SaveChanges();
                return StatusCode(200);
            }
            else //id ist unbekannt oder wurde nicht gefunden CREATE
            {
                databaseContext.assignments.Add(assignment);
                databaseContext.SaveChanges();
                return StatusCode(201);
            }
        }
    }
}





