using AutoMapper;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers;

public class BaseApiController : ControllerBase
{
    protected readonly IRepository _repository;
    protected readonly Services.Interfaces.ILogger _logger;
    protected readonly IMapper _mapper;

    public BaseApiController(IRepository repository, Services.Interfaces.ILogger logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
}
