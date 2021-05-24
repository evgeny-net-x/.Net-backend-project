using System;
using Backend6.Models;

namespace Backend6.Services
{
    public interface IUserPermissionsService
    {
        Boolean CanEditPost(Post post);

        Boolean CanEditPostComment(PostComment postComment);

        Boolean CanEditOrDeleteTopic(ForumTopic topic);

        Boolean CanEditOrDeleteMessage(ForumMessage message);

        Boolean CanCreateMessageAttachment(ForumMessage message);
        Boolean CanDeleteMessageAttachment(ForumMessageAttachment attachment);
    }
}