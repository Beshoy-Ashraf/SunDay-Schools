using Application.ChurchMembers.Dtos;
using MediatR;

namespace Application.ChurchMembers.Queries.GetChurchMemberById;

public record GetChurchMemberByIdQuery(Guid Id) : IRequest<ChurchMemberDto>;
