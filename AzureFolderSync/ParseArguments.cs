using System;
using System.Collections.Generic;

/* Ключевые классы программы:
 * 
 * 1. Бизнес-логика: 
 *   - FolderSyncConfiguration
 *   - FolderSynchronizer
 *   Они используют AzureBlob, но никто знать об этом не должен
 *   - SyncException
 *     Именно это исключение должно пробрасываться FolderSynchronizer-ом наверх (потроха должны скрываться),
 *     Вызывающий код должен его перехватывать в методе TrySync и выводить его на экран, возможно, спрашивая
 *     что-то у пользователя, типа, а нужно ли ретраиться.
 * 2. Аргументы и их парсинг
 *  - Argument - класс, который хранит значение аргумента. Поменять значение извне конструктора не должно быть возможно
 *    поскольку это приведет к расползанию обязанностей
 *  - ArgumentProcessor - центральны класс, который отвечает за обработку и создание валидных аргументов
 *    из командной строки.
 *    Пока что именно он может знать о списке возможных аргументов (ключей) и валидировать их.
 *
 */

namespace AzureFolderSync
{
    class SampleToShowTheDesign
    {
        // Этот метод не может называться Main, поскольку в приложении он должен быть только 1 (горец, блин!)
        public static void Main2(string[] args)
        {
            // 1. Parse provided arguments, print help if they're invalid
            // List<Argument> parsedArguments;
            // if (!ParseArgumentsAndPrintHelpIfInvalid(args, out parsedArguments) {
            // {
            //     Пусть принтит сообщение сам метод ParseArguments! В противном случае, нужно будет
            //     протаскивать сюда строку, ведь мы не всегда знаем, что именно нам нужно вывести на экран
            //     в следующих случаях!
            //     return;
            // }
            //
            // if (!MergeWitDefaultArguments(parsedArguments)) {
            //   return;
            // }
            // Если возможа валидация данных в app.cofnig, то можно использовать тот же
            // подход, что и с ParseArguments, т.е. возвращать false и выводить сообщение, что конфиг неправильный
            // FolderSyncConfiguration configuration;
            // if (!CreateSyncConfigurationAndPrintMessageIfSomethingWasWrong(parsesdArguments)) {
            //   return;
            // }
            // 
            // if (!TrySync(configuration)) {
            //   return -1;
            //}
            // 
            // return 0; // Проверить, какой код возврата должен быть в случае успеха/неудачи! Я забыл, что должно быть когда!
        }
    }

    // 1. Coupling - насколько сущности связанны друг с другом (внешние связи)
    // 2. Cohesion - насколько сущность является цельной

    // У нас есть тип аргумента! Давай его добавим

    public enum ArgumentKind
    {
        Boolean,
        String,
        Number,
    }

    public class Argument
    {
        private string _stringValue;
        private int _intValue;

        // Только свойства, не поля.
        // Почему: вполне возможно, что нам понадобится добавить дополнительное поведение
        // Сделать объект "неизменяемым", т.е. добавить конструкор, который будет инициализировать нужное состояние

        private Argument(string name, string description, ArgumentKind kind)
        {
            Name = name;
            Description = description;
            Kind = kind;
        }

        /// <summary>
        /// Name of the argument (without '-' or '/').
        /// </summary>
        public string Name { get; }

        public string Description { get; }

        public ArgumentKind Kind { get; }

        public string StringValue
        {
            get
            {
                if (Kind != ArgumentKind.String)
                {
                    throw new InvalidOperationException($"You're doing something stupid! This argument is not a string kind, but '{Kind}'");
                }

                return _stringValue;
            }
        }

        //public static Argument StringArgument(string name, string description, string value)
        //{
        //    return new Argument(name, description, ArgumentKind.String) {_stringValue = value};
        //}

        //public static Argument IntArgument(string name, string description, int value)
        //{
        //    return new Argument(name, description, ArgumentKind.String) {_intValue = value};
        //}

        //public static Argument FlagArgument(string name, string description, int value)
        //{
        //    return new Argument(name, description, ArgumentKind.String) {_intValue = value};
        //}
    }

    // Класс дожлен быть существительным, метод - глаголом. Поэтому класс - парсер, метод - parse/read
    public class ArgumentProcessor // если здесь останется PrintHelp, то это уже будет не парсер, а скорее processor
    {
        public static bool Parse(string[] args, out List<Argument> parsedArguments)
        {
            throw new NotImplementedException();
        }

        public static void PrintHelp()
        {
            //
        }

        private static List<Argument> InitArguments()
        {
            throw new NotImplementedException();
        }
    }


    public class ParseArguments
    {
        public ParseArguments()
        {
        }

        // Compare command line arguments with based parameters class
        public static Arguments[] ReadParams(string[] args, Arguments[] basedParams)
        {
            // TODO: code is working, but I'm not proud of it! Fix it later.
            int argNumber = 0;
            bool valueIsFound = false;

            do
            {
                // We found a key started with '-'
                if (args[argNumber][0] == '-') 
                {

                    for (int paramNumber = 0; paramNumber < basedParams.Length; paramNumber++)
                    {
                        if (args[argNumber].ToLower() == basedParams[paramNumber].paramKey)
                        {
                            // Boolean parameter
                            if (basedParams[paramNumber].isBool == true)
                            {
                                if (basedParams[paramNumber].isDefault == true)
                                {

                                    basedParams[paramNumber].boolValue = true;
                                    basedParams[paramNumber].isDefault = false;
                                    break;
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException("Duplicated argument " + basedParams[paramNumber].paramKey);
                                }
                            }
                            else
                            {
                                // String parameter
                                if (argNumber < args.Length - 1)   // If the value is exists ..
                                {
                                    if (args[argNumber + 1][0] == '-')  // If the next argument is also a key - exception!
                                    {
                                        throw new ArgumentOutOfRangeException("Missed value for the key" + basedParams[paramNumber].paramKey);
                                        
                                    }
                                    else
                                    {
                                        if (basedParams[paramNumber].isDefault == true)
                                        {
                                            basedParams[paramNumber].paramValue = args[argNumber + 1];
                                            basedParams[paramNumber].isDefault = false;
                                            valueIsFound = true;
                                            break;
                                        }
                                        else
                                        {
                                            throw new ArgumentOutOfRangeException("Duplicated argument " + basedParams[paramNumber].paramKey);
                                        }

                                    }
                                }
                                else           
                                {
                                    throw new ArgumentOutOfRangeException("Missed value for the key" + basedParams[paramNumber].paramKey);
                                    
                                }
                            }
                        }
                    }

                }
                else                   // We found an argument without '-'. 
                {
                    if (! valueIsFound)    // If it is not a value for previously found key - it's maybe a value for empty key
                    {
                        for (int paramNumber = 0; paramNumber < basedParams.Length; paramNumber++)
                        {
                            // Looking for a parameter with empty key
                            if (basedParams[paramNumber].paramKey == "")
                            {
                                // Check if the value is already found
                                if (basedParams[paramNumber].isDefault == true)
                                {

                                    basedParams[paramNumber].paramValue = args[argNumber];
                                    basedParams[paramNumber].isDefault = false;
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException("Duplicated argument "+ basedParams[paramNumber].paramKey);
                                }

                            }
                        }
                    }
                    valueIsFound = false;
                }

                argNumber++;
            }
            while (argNumber < args.Length);

            return basedParams;

        }

    }
}
