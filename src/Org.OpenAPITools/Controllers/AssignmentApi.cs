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
            var assignments = databaseContext.assignments;
            return new JsonResult(assignments);
          
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(PersonalAssignmentsGet200Response));
            string exampleJson = null;
            exampleJson = "{\n  \"assignments\" : [ {\n    \"reservation_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n    \"role\" : \"service\",\n    \"employee_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n    \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n  }, {\n    \"reservation_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n    \"role\" : \"service\",\n    \"employee_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n    \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n  } ]\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<PersonalAssignmentsGet200Response>(exampleJson)
            : default(PersonalAssignmentsGet200Response);
            //TODO: Change the data returned
            return new ObjectResult(example);
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
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);
            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401, default(Error));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(Error));

            throw new NotImplementedException();
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

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Assignment));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(Error));
            string exampleJson = null;
            exampleJson = "{\n  \"reservation_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"role\" : \"service\",\n  \"employee_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Assignment>(exampleJson)
            : default(Assignment);
            //TODO: Change the data returned
            return new ObjectResult(example);
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

            if (assignment.id == Guid.Empty) { return StatusCode(422); }

            if (assignment.id == id)
            {
                var old_ass = databaseContext.assignments.Find(assignment.id);
                if (old_ass != null) //id gegeben und wurde gefunden UPDATE
                {
                    old_ass.reservation_id = assignment.reservation_id;
                    old_ass.role= assignment.role;
                    old_ass.employee_id= assignment.employee_id;

                    databaseContext.Update(old_ass);
                }
                else //id ist unbekannt oder wurde nicht gefunden CREATE
                {
                    databaseContext.assignments.Add(assignment);
                }
                databaseContext.SaveChanges();
                return StatusCode(204);


            }
            else return StatusCode(422, "Mismatch in ID and Object");
        }

        //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(204);
        //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(400, default(Error));
        //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(401, default(Error));
        //TODO: Uncomment the next line to return response 422 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(422, default(Error));




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
        public virtual  IActionResult PersonalAssignmentsPost([FromBody] Assignment assignment)
        {

            if (assignment.id == Guid.Empty)
            {
                //No id is given in request body So it creates assig id, pushes assignment to database 
                assignment.id = Guid.NewGuid();
            }
            else if ((assignment.role!="service") &(assignment.role !="cleanup"))
            {
                //keine richtige rolle
                return StatusCode(422, "NO valid Role");
            }


            //HTTP abfrage an employee
            try
            {
               // HTTPAbfrage("http://localhost:8000/personal/employees/"+ assignment.employee_id);
            }
            catch (HttpRequestException) { return StatusCode(422, "Employee not found"); }

            try
            {
                 //var Response = HTTPAbfrage("http://localhost:8000/reservations/"+ assignment.reservation_id);
            }
            catch (HttpRequestException) { return StatusCode(422, "reservation not found"); }

            

                /*
    # reservation has assignment with same role?
    existing_assignment_reservations: list[Assignments] = session.query(Assignments).filter(Assignments.reservation_id==assignment.reservation_id).all()
        if existing_assignment_reservations != []:
            for existing_assignment_reservation in existing_assignment_reservations:
                if existing_assignment_reservation.role == assignment.role:
                    raise HTTPException(status_code= status.HTTP_422_UNPROCESSABLE_ENTITY)

        # assignment with same id existing?
        existing_assignment = session.query(Assignments).filter(Assignments.id==assignment.id).first()
        if existing_assignment:
            #UPDATES assignment, pus,token: bool = Depends(validate)hes assignment to database
            db_assignment = session.get(Assignments, assignment.id)
            db_assignment.employee_id = assignment.employee_id
            db_assignment.reservation_id = assignment.reservation_id
            db_assignment.role = assignment.role
            session.add(db_assignment)
            session.commit()
            raise HTTPException(status_code= status.HTTP_200_OK, detail= "")
        else:
            #CREATES assignment, pushes assignment to database
            db_assignment = Assignments.from_orm(assignment)
            session.add(db_assignment)
            session.commit()
            raise HTTPException(status_code= status.HTTP_201_CREATED)






                databaseContext.asssignments.Add(assignment);
                databaseContext.SaveChanges();
                var exis_empl = databaseContext.employees.Find(employee.id);
                if (exis_empl != null) //id gegeben und wurde gefunden UPDATE
                {
                    exis_empl.name = assignment.name;
                    databaseContext.Update(exis_empl);
                    databaseContext.SaveChanges();
                }
                else //id ist unbekannt oder wurde nicht gefunden CREATE
                {
                    databaseContext.employees.Add(employee);
                    databaseContext.SaveChanges();
                } 

                 */

                //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
                // return StatusCode(200, default(Assignment));
                //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
                // return StatusCode(201, default(Assignment));
                //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
                // return StatusCode(400, default(Error));
                //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
                // return StatusCode(401, default(Error));
                //TODO: Uncomment the next line to return response 422 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
                // return StatusCode(422, default(Error));
                string exampleJson = null;
            exampleJson = "{\n  \"reservation_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"role\" : \"service\",\n  \"employee_id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Assignment>(exampleJson)
            : default(Assignment);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /*public async Task<string> HTTPAbfrage(string url)
        {
            // Erstellen Sie eine neue HttpClient-Instanz
            var client = new HttpClient();


            // Senden Sie eine GET-Anfrage an die angegebene URL
            var response = await client.GetAsync(url);

            // Warten Sie, bis die Antwort empfangen wurde
            response.EnsureSuccessStatusCode(); // Wirft eine Ausnahme, wenn der Statuscode nicht in der 2xx-Range liegt

            // Lesen Sie den Antworttext als Zeichenkette
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }*/
    }
}


