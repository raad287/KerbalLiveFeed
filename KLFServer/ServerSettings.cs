﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;

namespace KLFServer
{
	public class ServerSettings
	{
		public const String SERVER_CONFIG_FILENAME = "KLFServerConfig.txt";
		public const String PORT_LABEL = "port";
		public const String MAX_CLIENTS_LABEL = "maxClients";
		public const String JOIN_MESSAGE_LABEL = "joinMessage";
		public const String UPDATE_INTERVAL_LABEL = "updateInterval";
		public const String AUTO_RESTART_LABEL = "autoRestart";

		public int port = 2075;
		public int maxClients = 32;
		public int updateInterval = 500;
		public bool autoRestart = false;
		public String joinMessage = String.Empty;

		public const int MIN_UPDATE_INTERVAL = 20;
		public const int MAX_UPDATE_INTERVAL = 5000;

		//Config

		public void readConfigFile()
		{
			try
			{
				TextReader reader = File.OpenText(SERVER_CONFIG_FILENAME);

				String line = reader.ReadLine();

				while (line != null)
				{
					String label = line; //Store the last line read as the label
					line = reader.ReadLine(); //Read the value from the next line

					if (line != null)
					{
						//Update the value with the given label
						if (label == PORT_LABEL)
						{
							int new_port;
							if (int.TryParse(line, out new_port) && new_port >= IPEndPoint.MinPort && new_port <= IPEndPoint.MaxPort)
								port = new_port;
						}
						else if (label == MAX_CLIENTS_LABEL)
						{
							int new_max;
							if (int.TryParse(line, out new_max) && new_max > 0)
								maxClients = new_max;
						}
						else if (label == JOIN_MESSAGE_LABEL)
						{
							joinMessage = line;
						}
						else if (label == UPDATE_INTERVAL_LABEL)
						{
							int new_val;
							if (int.TryParse(line, out new_val) && new_val >= MIN_UPDATE_INTERVAL && new_val <= MAX_UPDATE_INTERVAL)
								updateInterval = new_val;
						}
						else if (label == AUTO_RESTART_LABEL)
						{
							bool new_val;
							if (bool.TryParse(line, out new_val))
								autoRestart = new_val;
						}

					}

					line = reader.ReadLine();
				}

				reader.Close();
			}
			catch (FileNotFoundException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}

		}

		public void writeConfigFile()
		{
			TextWriter writer = File.CreateText(SERVER_CONFIG_FILENAME);

			//port
			writer.WriteLine(PORT_LABEL);
			writer.WriteLine(port);

			//max clients
			writer.WriteLine(MAX_CLIENTS_LABEL);
			writer.WriteLine(maxClients);

			//join message
			writer.WriteLine(JOIN_MESSAGE_LABEL);
			writer.WriteLine(joinMessage);

			//update interval
			writer.WriteLine(UPDATE_INTERVAL_LABEL);
			writer.WriteLine(updateInterval);

			//auto-restart
			writer.WriteLine(AUTO_RESTART_LABEL);
			writer.WriteLine(autoRestart);

			writer.Close();
		}
	}
}