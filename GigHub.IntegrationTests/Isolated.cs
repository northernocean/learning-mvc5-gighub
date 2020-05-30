using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Transactions;

namespace GigHub.IntegrationTests
{
    class Isolated : Attribute, ITestAction
    {

        private TransactionScope _transactionScope;

        ActionTargets ITestAction.Targets => ActionTargets.Test;

        void ITestAction.BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }

        void ITestAction.AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

    }
}
