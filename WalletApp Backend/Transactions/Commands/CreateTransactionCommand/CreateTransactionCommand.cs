using Application.Models;
using FluentValidation;
using MapsterMapper;
using MediatR;
using WalletApp_Backend.Common.Validation;
using WalletApp_Backend.DataBase;
using WalletApp_Backend.Transactions.Entity;
using WalletApp_Backend.User.Services;

namespace WalletApp_Backend.Transactions.Commands.CreateTransactionCommand
{
    public record CreateTransactionCommand(TransactionType Type, decimal Value, string Name, string Description, DateTime CreationDate, string? ApproveUserId,string Icon) : IRequest<Response<int>>;


    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private IValidator<CreateTransactionCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public CreateTransactionCommandHandler(ApplicationDbContext context, IMapper mapper, IValidator<CreateTransactionCommand> validator, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _authService = authService;
        }

        public async Task<Response<int>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return FailureResponses.BadRequest<int>(validationResult.ErrorMessages());

            var transaction = _mapper.Map<Transaction>(request);
            transaction.Status=TransactionStatus.Pending;
            transaction.CreatedById=_authService.GetCurrentUserId();

            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return SuccessResponses.Ok(transaction.Id);
        }
    }
}