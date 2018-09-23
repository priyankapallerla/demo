
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Infrastructure;

namespace WebApi.Features.Employees
{
    /// <summary>
    /// Find an employee
    /// </summary>
    /// <param name="id">id to search for</param>
    /// <returns>
    /// Returns <see cref="EmployeeViewModel"/>
    /// </returns>
    public class Details
    {
        public class Query : IRequest<EmployeeViewModel>
        {
            // Parameter to pass in query
            public Query(Guid id)
            {
                Id = id;
            }

            // Create a property from parameter
            public Guid Id { get; }
        }

        public class QueryHandler : IRequestHandler<Query, EmployeeViewModel>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public QueryHandler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<EmployeeViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    // Find employee, get the Id param from Query
                    var model =  await _context.Employees.FindAsync(request.Id);

                    // Map model to view model
                    return _mapper.Map<EmployeeViewModel>(model);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}