using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Principal;

namespace CommunityWebsite_Lexicon_Project.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public Task AddAsync(Event theEvent)
        //{
        //    if (!string.IsNullOrEmpty(theEvent.Message))
        //    {
        //        _context.Events.Add(theEvent);
        //        return _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        throw new Exception("You must supply a message.");
        //    }
        //}

        //public async Task<Event> GetEventMatchingEventIdAsync(Guid id)
        //{
        //    return await _context.Events.SingleOrDefaultAsync(x => x.EventId == id);
        //}

        //public List<Event> GetEventsAccountIsAttending(Account account)
        //{
        //    return _context.Events.Where(x => x.UsersAttending.Contains(account)).ToList();
        //}

        //public List<Event> GetEventsByMatchingAccountUserName(string username)
        //{
        //    return _context.Events.Where(x => x.OriginalPoster.UserName == username).ToList();
        //}

        //public List<Event> GetEventsByMatchingEmail(string email)
        //{
        //    return _context.Events.Where(x => x.OriginalPoster.Email == email).ToList();
        //}

        //public List<Event> GetEventsHappeningInYear(DateTime dateTime)
        //{
        //    return _context.Events.Where(x => x.CreationDateTime.Year == dateTime.Year).ToList();
        //}

        //public List<Event> GetEventsHappeningOnDay(DateTime dateTime)
        //{
        //    return _context.Events.Where(x => x.CreationDateTime.Day == dateTime.Day).ToList();
        //}

        //public List<Event> GetEventsHappeningOnMonth(DateTime dateTime)
        //{
        //    return _context.Events.Where(x => x.CreationDateTime.Month == dateTime.Month).ToList();
        //}

        //public List<Event> GetEventsHappeningOnWeek(DateTime dateTime)
        //{
        //    //int weekOfYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
        //    //    dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        //    //return _context.Events.Where(x => x.CreationDateTime. == dateTime.Month).ToList();
        //}
    }
}
