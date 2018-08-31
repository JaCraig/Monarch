# Monarch

[![Build status](https://ci.appveyor.com/api/projects/status/am86kwcnwmyj21ra?svg=true)](https://ci.appveyor.com/project/JaCraig/monarch)


Monarch is a command line parser/task runner.

## Basic Usage

The system relies on an IoC wrapper called [Canister](https://github.com/JaCraig/Canister). While Canister has a built in IoC container, it's purpose is to actually wrap your container of choice in a way that simplifies setup and usage for other libraries that don't want to be tied to a specific IoC container. Monarch uses it to detect and pull in various info. As such you must set up Canister in order to use Monarch:

    Canister.Builder.CreateContainer(null)
					.AddAssembly(...) //Add your current assembly so that commands can be discovered by the system.
                    .RegisterMonarch()
                    .Build();
					
This is required prior to using the Monarch class for the first time. Once Canister is set up, you can use the CommandRunner class:

    var Instance = new CommandRunner();
	return Instance.Run(args);
	
The CommandRunner class has a Run method which parses the args passed in and runs the appropriate command. The library has help and version commands built in to the system, however to create your own commands you need to create a command and also an input class:

    public class TestCommand : CommandBaseClass<TestInput>
    {
        public override string[] Aliases => new string[] { "Test" };

        public override string Description => "Test command";

        public override string Name => "Test Command";

        protected override async Task<int> Run(TestInput input)
        {
            await Task.CompletedTask;
            Console.WriteLine(input.Value1);
            Console.WriteLine(input.Value2);
            Console.WriteLine(input.Value3.ToString(x => x));
            return 0;
        }
    }
	
	public class TestInput
    {
        [Display(Description = "Value 1 Property")]
        public int Value1 { get; set; }

        [Display(Description = "Value 2 Property")]
        public string Value2 { get; set; }

        [Display(Description = "Value 3 Property")]
        [MaxLength(3)]
        public List<string> Value3 { get; set; }
    }
	
The command above inherits from the CommandBaseClass and defines the input that it expects. In this case TestInput. TestInput is how the command line arguments should be parsed by the system. The commands must define the aliases, description, and name for the command. The TestInput then defines properties and uses the DisplayAttribute from the System.ComponentModel.DataAnnotations namespace to define information. Also any data annotations defining max length, required, etc. are used to validate the input.

## Options

By default the system does not require you to set options. However you can specify some options found within the system including command prefix, flag prefix, and indentation amount. In order to override what is in the system just create a class that inherits from IOptions:

    public class DefaultOptions : IOptions
    {
        /// <summary>
        /// Gets the command prefix.
        /// </summary>
        /// <value>The command prefix.</value>
        public string CommandPrefix { get; } = "";

        /// <summary>
        /// Gets the flag prefix.
        /// </summary>
        /// <value>The flag prefix.</value>
        public string FlagPrefix { get; } = "-";

        /// <summary>
        /// Gets the indent amount.
        /// </summary>
        /// <value>The indent amount.</value>
        public int IndentAmount { get; } = 4;
    }