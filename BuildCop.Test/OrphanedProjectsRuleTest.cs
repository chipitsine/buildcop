using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BuildCop.Reporting;
using BuildCop.Configuration;
using BuildCop.Rules;

namespace BuildCop.Test
{
    [TestClass]
	public class OrphanedProjectsRuleTest
	{
		[TestMethod]
		[DeploymentItem("BuildFiles", "BuildFiles")]
		public void ProjectInSolutionShouldBeFound()
		{
			BuildFile file = new BuildFile(@"BuildFiles\DefaultConsoleApplication.csproj");
			ruleElement rule = new ruleElement();
			rule.solutions.searchPath = "BuildFiles";
			rule.RuleChecker = new OrphanedProjectsRule(rule);
			rule.name = "OrphanedProjects";
			IList<LogEntry> entries = rule.RuleChecker.Check(file);
			Assert.IsNotNull(entries);
			Assert.AreEqual<int>(0, entries.Count);
		}

		[TestMethod]
		[DeploymentItem("BuildFiles", "BuildFiles")]
		public void ProjectNotInSolutionShouldBeError()
		{
			BuildFile file = new BuildFile(@"BuildFiles\SignedConsoleApplication.csproj");
			ruleElement rule = new ruleElement();
			rule.solutions.searchPath = "BuildFiles";
			rule.RuleChecker = new OrphanedProjectsRule(rule);
			rule.name = "OrphanedProjects";
			IList<LogEntry> entries = rule.RuleChecker.Check(file);
			Assert.IsNotNull(entries);
			Assert.AreEqual<int>(1, entries.Count);
			LogEntry entry = entries[0];
			Assert.AreEqual<LogLevel>(LogLevel.Error, entry.Level);
			Assert.AreEqual<string>("OrphanedProjects", entry.Rule);
			Assert.AreEqual<string>("OrphanedProject", entry.Code);
		}
	}
}
