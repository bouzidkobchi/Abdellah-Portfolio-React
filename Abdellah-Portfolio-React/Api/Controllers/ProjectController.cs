using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Repo = Abdellah_Portfolio_React.Data.Repositories.ProjectRepository;

namespace Abdellah_Portfolio_React.Api.Controllers
{
    [ApiController]
    public class ProjectController : Controller
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("/api/projects")]
        public JsonResult Add([Required] string projectName, string about, [Required][Url] string githubUrl)
        {
            // 200
            var project = new Project { ProjectName = projectName, About = about, GithubUrl = githubUrl };
            int projectId = Repo.Add(project);

            var response = Json(new
            {
                message = "project added successfuly .",
                projectId
            });
            response.StatusCode = 200;
            return response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("/api/projects")]
        public JsonResult Update([Required] int projectId)
        {
            var project = Repo.GetById(projectId);
            JsonResult response;

            // 404
            if (project is null)
            {
                response = Json(new
                {
                    message = "project not found ."
                });
                response.StatusCode = 404;
                return response;
            }

            // 200
            Repo.Update(project);
            response = Json(new
            {
                messgae = "project updated successfuly ."
            });

            response.StatusCode = 200;
            return response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("/api/projects")]
        public JsonResult Delete([Required] int projectId)
        {
            var project = Repo.GetById(projectId);
            JsonResult response;

            // 404
            if (project is null)
            {
                response = Json(new
                {
                    message = "project not found ."
                });
                response.StatusCode = 404;
                return response;
            }

            // 200
            Repo.Delete(project);
            response = Json(new
            {
                message = "project deleted successfuly ."
            });
            return response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("/api/projects")]
        public JsonResult GetPage(int page = 0, int pageSize = 1)
        {
            // 200
            var response = Json(new
            {
                projects = Repo.GetPage(page, pageSize)
            });
            return response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("/api/projects/{id:int}")]
        public JsonResult Get(int id)
        {
            var project = Repo.GetById(id);
            JsonResult response;

            // 404
            if (project is null)
            {
                response = Json(new
                {
                    message = "project not found ."
                });
                response.StatusCode = 404;
                return response;
            }

            // 200
            response = Json(new
            {
                project
            });
            return response;
        }
    }
}
