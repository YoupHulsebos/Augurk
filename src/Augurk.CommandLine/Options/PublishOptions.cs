﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Augurk.CommandLine.Options
{
    /// <summary>
    /// Represents the available command line options when publishing features.
    /// </summary>
    internal class PublishOptions
    {
        /// <summary>
        /// Name of the verb for this set of options.
        /// </summary>
        public const string VERB_NAME = "publish";

        /// <summary>
        /// Gets or sets the set of .feature files that should be published to Augurk.
        /// </summary>
        [OptionList("featureFiles", Separator = ',', HelpText = "Set of feature files that should be published to Augurk.", Required = true)]
        public IEnumerable<string> FeatureFiles { get; set; }

        /// <summary>
        /// Gets or sets the URL of the instance of Augurk to which the features files should be published.
        /// </summary>
        [Option("url", HelpText = "URL to the Augurk Instance to which the features files should be published.", Required = true)]
        public string AugurkUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch under which the feature files should be published.
        /// </summary>
        [Option("branchName", HelpText = "Name of the branch under which the feature files should be published.", Required = true)]
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets the name of the group under which the feature files should be published.
        /// </summary>
        [Option("groupName", HelpText = "Name of the group under which the feature files should be published.", Required = true)]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the group should be cleared prior to publishing the new features.
        /// </summary>
        [Option("clearGroup", HelpText = "If set the group specified by --groupName will be cleared prior to publishing the new features.", Required = true)]
        public bool ClearGroup { get; set; }

        /// <summary>
        /// Gets or sets the language in which the feature files have been written.
        /// </summary>
        [Option("language", HelpText = "Language in which the features files have been written. For example: `en-US` or `nl-NL`.", DefaultValue = "en-US", Required = false)]
        public string Language { get; set; }
    }
}
