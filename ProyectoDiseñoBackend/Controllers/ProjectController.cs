using Microsoft.AspNetCore.Mvc;
using ProyectoDiseñoBackend.Iterador;
using ProyectoDiseñoBackend.Modelos;
using ProyectoDiseñoBackend.Servicios;

namespace ProyectoDiseñoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService) =>
            _projectService = projectService;

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> Get()
        {
            var project = await _projectService.GetAsync();
            var projectCollection = new GenericCollection<Project>(project);
            var iterator = projectCollection.CreateIterator();

            var projectList = new List<Project>();
            while (iterator.HasNext())
            {
                projectList.Add(iterator.Next());
            }

            return Ok(projectList);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<ActionResult<Project>> Get(string id)
        {
            var project = await _projectService.GetAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Project newProject)
        {
            await _projectService.CreateAsync(newProject);
            return CreatedAtAction(nameof(Get), new { id = newProject.Id }, newProject);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Project updatedProject)
        {
            var project = await _projectService.GetAsync(updatedProject.Id);

            if (project is null)
            {
                return NotFound();
            }

            updatedProject.Id = project.Id;
            await _projectService.UpdateAsync(updatedProject.Id, updatedProject);
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var project = await _projectService.GetAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            await _projectService.RemoveAsync(id);
            return NoContent();
        }
    }
}
