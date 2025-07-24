using System.Linq.Expressions;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Expressions;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Specifications;

public class PilotFilterSpecifiation
{
    public static Expression<Func<Pilot, bool>> AsExpression(PilotFilterDto filterDto)
    {
        Expression<Func<Pilot, bool>> filter = e => true;

        if (!string.IsNullOrEmpty(filterDto.Name))
            filter = filter.AndAlso(e => e.Name.Contains(filterDto.Name));

        if (!string.IsNullOrEmpty(filterDto.Number))
            filter = filter.AndAlso(e => e.Number.Contains(filterDto.Number));

        if (!string.IsNullOrEmpty(filterDto.Code))
            filter = filter.AndAlso(e => e.Code.Contains(filterDto.Code));

        if (!string.IsNullOrEmpty(filterDto.Nationality))
            filter = filter.AndAlso(e => e.Nationality.Contains(filterDto.Nationality));

        return filter;
    }
}