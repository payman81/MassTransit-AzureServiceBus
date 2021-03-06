﻿using System;
using MassTransit.Util;

namespace MassTransit.Transports.AzureServiceBus
{
	/// <summary>
	/// The envelope that we're shoving into AppFabric ServiceBus Queues.
	/// </summary>
	[Serializable]
	public class MessageEnvelope
	{
		/// <summary>
		/// c'tor
		/// </summary>
		public MessageEnvelope([NotNull] byte[] actualBody)
		{
			if (actualBody == null) throw new ArgumentNullException("actualBody");
			ActualBody = actualBody;
		}

		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected MessageEnvelope()
		{
		}

		/// <summary>
		/// Gets the actual byte[] of the body.
		/// </summary>
		public byte[] ActualBody { get; protected set; }
	}
}