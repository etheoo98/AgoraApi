﻿using Thread = Domain.Entities.Thread;

namespace Domain.Interfaces.Repositories;

public interface IThreadRepository
{
    Task AddThread(Thread thread);
}