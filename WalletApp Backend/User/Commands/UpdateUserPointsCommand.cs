using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WalletApp_Backend.Common.Extensions;
using WalletApp_Backend.DataBase;

namespace WalletApp_Backend.User.Commands
{
    public record UpdateUserPointsCommand() : IRequest<Response<EmptyValue>>;


    public class UpdateUserPointsCommandHandler : IRequestHandler<UpdateUserPointsCommand, Response<EmptyValue>>
    {
        private readonly ApplicationDbContext _context;
        public UpdateUserPointsCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<EmptyValue>> Handle(UpdateUserPointsCommand request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);

            var daysCount = DateTime.Now.CountDayFromTodaySeasons();
            var points = (int)CalculatePointsForSeasons(daysCount);
            foreach (var user in users)
            {
                user.Points += points;
            }

            _context.Users.UpdateRange(users);
            await _context.SaveChangesAsync(cancellationToken);

            return SuccessResponses.Ok();
        }
        private double CalculatePointsForSeasons(List<int> daysCount)
        {
            var points = 0d;
            foreach (var count in daysCount)
            {
                points += CalculatePoints(count);
            }

            return points;
        }

        private double CalculatePoints(int count)
        {
            int firstDayPoints = 2;
            int secondDayPoints = 3;
            if (count == 1) return firstDayPoints;
            if (count == 2) return secondDayPoints;

            double previous1Points = secondDayPoints;
            double previous2Points = firstDayPoints;
            double points = 0;

            for (int i = 3; i <= count; i++)
            {
                points = (previous1Points * 0.6) + previous2Points;
                previous2Points = previous1Points;
                previous1Points = points;
            }

            return points;
        }

    }
}