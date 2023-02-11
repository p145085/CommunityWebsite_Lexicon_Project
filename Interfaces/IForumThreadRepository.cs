﻿using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Interfaces
{
    public interface IForumThreadRepository
    {
        Task<ForumThread> GetForumThreadMatchingEventIdAsync(string id);
        List<ForumThread> GetForumThreadsByMatchingAccountUserName(string username);
        List<ForumThread> GetForumThreadsByMatchingEmail(string email);
        Task AddAsync(ForumThread forumThread);
    }
}