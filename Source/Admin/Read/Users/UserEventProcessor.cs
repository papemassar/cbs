/*---------------------------------------------------------------------------------------------
 *  Copyright (c) The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Events.Processing;
using Dolittle.ReadModels;
using Events.Users;

namespace Read.Users
{
    public class UserEventProcessor : ICanProcessEvents
    {
        readonly IReadModelRepositoryFor<User> _users;

        public UserEventProcessor(IReadModelRepositoryFor<User> users)
        {
            _users = users;
        }
        [EventProcessor("2b354035-128e-470f-b0ef-e5cc449b0ebb")]
        public void Process(UserCreated @event)
        {
            _users.Insert(new User
            {
                Country = @event.Country,
                FullName = @event.FullName,
                Id = @event.Id,
                NationalSocietyId = @event.NationalSocietyId,
                DisplayName = @event.DisplayName
            });
        }
    }
}