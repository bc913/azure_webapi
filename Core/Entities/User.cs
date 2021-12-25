using System;
using Bcan.Backend.SharedKernel;
using Bcan.Backend.SharedKernel.Contracts;
using Ardalis.GuardClauses;
using Bcan.Backend.Core.ValueObjects;

namespace Bcan.Backend.Core.Entities
{
    public sealed class User : BaseEntity<Guid>, IAggregateRoot
    {
        public FullName FullName { get; private set; }
        public string NickName { get; private set; }

        public User(Guid id, FullName fullName, string nickName) : base(id)
        {
            NickName = Guard.Against.NullOrWhiteSpace(nickName, nameof(nickName));
            FullName = Guard.Against.Null<FullName>(fullName, nameof(fullName));
        }
    }
}