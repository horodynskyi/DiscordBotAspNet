﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Administrator
	{
		public string Id { get; set; }
		public string Nickname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool ConfirmEmail { get; set; }
		public ulong GuildId { get; set; }
	}
}
