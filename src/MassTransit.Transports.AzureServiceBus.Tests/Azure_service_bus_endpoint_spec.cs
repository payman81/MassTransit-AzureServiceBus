﻿// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using Magnum.TestFramework;
using MassTransit.Testing;
using MassTransit.Transports.AzureServiceBus.Tests.Framework;
using NUnit.Framework;

namespace MassTransit.Transports.AzureServiceBus.Tests
{
	[Description("This test verifies that one can use the testing framework with Azure Service Bus infrastructure"),
	 NotWorking("Because ASB is too slow to finish within the 8 allotted seconds")]
	public class Handler_test_factory_contract
	{
		HandlerTest<A> _test;

		[When]
		public void Setup()
		{
			_test = TestFactory.ForHandler<A>()
				.New(x =>
				{
					x.UseAzureServiceBusBusScenario(new AccountDetails());

					x.Send(new A());
					x.Send(new B());
				});

			_test.Execute();
		}

		[Finally]
		public void Teardown()
		{
			_test.Dispose();
			_test = null;
		}

		[Then]
		public void Should_have_received_a_message_of_type_a()
		{
			_test.Received.Any<A>().ShouldBeTrue();
		}

		[Then]
		public void Should_have_skipped_a_message_of_type_b()
		{
			_test.Skipped.Any<B>().ShouldBeTrue();
		}

		[Then]
		public void Should_not_have_skipped_a_message_of_type_a()
		{
			_test.Skipped.Any<A>().ShouldBeFalse();
		}

		[Then]
		public void Should_have_sent_a_message_of_type_a()
		{
			_test.Sent.Any<A>().ShouldBeTrue();
		}

		[Then]
		public void Should_have_sent_a_message_of_type_b()
		{
			_test.Sent.Any<B>().ShouldBeTrue();
		}

		[Then]
		public void Should_support_a_simple_handler()
		{
			_test.Handler.Received.Any().ShouldBeTrue();
		}

		class A
		{
		}

		class B
		{
		}
	}
}