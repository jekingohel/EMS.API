using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Models;

public class CompanyInfo : Company
{
	public string Departments { get; set; }
}

