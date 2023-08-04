﻿using BlogService.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;

namespace BlogService.Service.UserService
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Node> nodeRepository;
        private readonly IBaseRepository<Edge> edgeRepository;

        public UserService( 
            IBaseRepository<User> userRepository,
            IBaseRepository<Node> nodeRepository,
            IBaseRepository<Edge> edgeRepository
        ) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.nodeRepository = nodeRepository;
            this.edgeRepository = edgeRepository;
        }
    }
}