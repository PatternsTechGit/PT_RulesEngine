using Entities;
using Entities.Request;
using Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Actions;
using RulesEngine.Models;
using Services.Contracts;
using Services.RulesFunctions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RulesService : IRulesService
    {
        private readonly BBBankContext _bbBankContext;
        public RulesService(BBBankContext BBBankContext)
        {
            _bbBankContext = BBBankContext;
        }
        public async Task<RuleResult> IsTransferAllowed(TransferRequest transferRequest)
        {
            var rulesEngine = GetRulesEngineObj();
            // here "TransferRules" is name of workflow as defined in json, and transferRequest is input1 and _unitOfWork is input2
            var resultsPerRule = await rulesEngine.ExecuteAllRulesAsync("TransferRules", transferRequest, _bbBankContext);

            // if all the rules were sucess
            if (resultsPerRule.All(x => x.IsSuccess == true))
                return new RuleResult() { IsSuccess = true };
            else
            {
                // if any of teh rules fails, collects its errors by looping on it
                string errors = null;
                foreach (var rule in resultsPerRule)
                {
                    if (rule.IsSuccess == false)
                    {
                        errors += String.Join("##", rule.GetMessages().ErrorMessages);
                    }

                }
                return new RuleResult() { IsSuccess = false, Errors = errors };
            }
        }

        private Workflow[] ReadRulesFile()
        {
            // use code to read file in production
            StreamReader sr = new StreamReader("BBBankRules.json");
            var jsonString = sr.ReadToEnd();
            var workflows = JsonConvert.DeserializeObject<Workflow[]>(jsonString);
            return workflows;
        }

        private RulesEngine.RulesEngine GetRulesEngineObj()
        {
            var reSettings = new ReSettings
            {
                // here we mention static files used in after rules sucess
                CustomActions = new Dictionary<string, Func<ActionBase>>
                {
                    //        { "PostEval_AbsenceOfEvent", () => new PostEval_AbsenceOfEvent() }
                },
            };
            // this setting is only required if rule is using a static file
            reSettings.CustomTypes = new Type[] {
                typeof(Eval_TransferRules)
            };
            var workflows = ReadRulesFile();
            return new RulesEngine.RulesEngine(workflows, null, reSettings);
        }
    }
}
