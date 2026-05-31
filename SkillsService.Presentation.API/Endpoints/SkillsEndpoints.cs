using SkillsService.Application.Services;
using SkillsService.Domain.Exceptions;

namespace SkillsService.Presentation.API.Endpoints
{
    public static class SkillsEndpoints
    {
        public static void MapSkillsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/skills")
                .WithTags("Skills");

            group.MapGet("/catalog", GetCatalogAsync)
                .Produces(StatusCodes.Status200OK);

            group.MapGet("/{ownerId}", GetUserSkillsAsync)
                .Produces(StatusCodes.Status200OK);

            group.MapPost("/", AddUserSkillAsync)
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id:guid}", DeleteUserSkillAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<IResult> GetCatalogAsync(SkillsAppService service, CancellationToken ct)
        {
            var catalog = await service.GetCatalogAsync(ct);
            return Results.Ok(catalog);
        }

        private static async Task<IResult> GetUserSkillsAsync(string ownerId, SkillsAppService service, CancellationToken ct)
        {
            var skills = await service.GetUserSkillsAsync(ownerId, ct);
            return Results.Ok(skills);
        }

        private static async Task<IResult> AddUserSkillAsync(AddUserSkillRequest request, SkillsAppService service, CancellationToken ct)
        {
            try
            {
                var userSkill = await service.AddUserSkillAsync(request.OwnerId, request.SkillCatalogId, ct);
                return Results.Created($"/api/skills/{userSkill.Id}", userSkill);
            }
            catch(DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> DeleteUserSkillAsync(Guid id, SkillsAppService service, CancellationToken ct)
        {
            await service.DeleteUserSkillAsync(id, ct);
            return Results.NoContent();
        }
    }
    public record AddUserSkillRequest(string OwnerId, Guid SkillCatalogId);
}


