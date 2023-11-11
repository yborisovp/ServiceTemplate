using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceTemplate.DataAccess.Context;
using ServiceTemplate.DataAccess.Interfaces;
using ServiceTemplate.DataAccess.Models.Templates;
using ServiceTemplate.DataAccess.Models.Templates.Enums;

namespace ServiceTemplate.DataAccess.Repositories;

public class TemplateRepository : BaseRepository, ITemplateRepository
{
    private readonly ILogger<TemplateRepository> _logger;
    
    public TemplateRepository(IDatabaseContextFactory contextFactory, ILogger<TemplateRepository> logger) : base(contextFactory)
    {
        _logger = logger;
    }
    
    public async Task<IEnumerable<Template>> GetAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Get all {name of}s", nameof(Template));
        await using var context = ContextFactory.CreateDbContext();

        var templates = await GetFullQuery(context.Templates)
            .ToListAsync(ct);

        return templates;
    }
    
    public async Task<Template> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} by Id: '{templateId}' from database", nameof(Template), id);
        await using var context = ContextFactory.CreateDbContext();

        var template = await context.Templates.Where(t => t.Id == id).FirstOrDefaultAsync(ct);
        
        if (template is null)
        {
            throw new KeyNotFoundException($"{nameof(Template)} with Id: '{id}' not found");
        }

        return template;
    }

    public async Task<Template> UpdateAsync(Template entityToUpdate, CancellationToken ct = default)
    {
        _logger.LogDebug("Update {name of} with Id: '{id}' in database", nameof(Template), entityToUpdate.Id);
        
        await using var context = ContextFactory.CreateDbContext();
        context.Templates.Update(entityToUpdate);
        await context.SaveChangesAsync(ct);
        
        _logger.LogDebug("{name of} with Id: '{id}' - updated in database", nameof(Template), entityToUpdate.Id);
        
        return entityToUpdate;
    }

    public async Task<Guid> DeleteByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Delete {name of} with id: '{id}' in the database", nameof(Template), id);
        await using var context = ContextFactory.CreateDbContext();

        var entity = await context.Templates.Where(e => e.Id == id).FirstOrDefaultAsync(ct);
        if (entity != null)
        {
            context.Templates.Remove(entity);
        }
        else
        {
            throw new KeyNotFoundException($"{nameof(Template)} with provided id: '{id}' - not found in database");
        }
        
        await context.SaveChangesAsync(ct);

        _logger.LogDebug("{name of} with id {id} was deleted successfully", nameof(Template), id);
        return id;
    }

    public async Task<IEnumerable<Template>> GetTemplatesByEnumType(TemplateEnum templateEnum, CancellationToken ct = default)
    {
        _logger.LogDebug("Get {name of} by enum state: '{state}' from database", nameof(Template), templateEnum);
        await using var context = ContextFactory.CreateDbContext();

        var getTemplatesByEnumType = await context.Templates.Where(t => t.TemplateEnum == templateEnum).ToListAsync(ct);

        _logger.LogDebug("{name of} with enum state '{state}' was deleted successfully", nameof(Template), templateEnum);
        return getTemplatesByEnumType;
    }
    
    private static IQueryable<Template> GetFullQuery(IQueryable<Template> form)
    {
        //Remove when fix https://github.com/dotnet/efcore/issues/21663
#pragma warning disable CS8620
        return form;
#pragma warning restore CS8620
    }
}