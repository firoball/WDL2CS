using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace VCCCompiler
{
        /// <summary>
        /// Zusammenfassung für MyCompiler.
        /// </summary>
        class MyCompiler
        {
                YYARec[] yya;
                YYARec[] yyg;
                YYRRec[] yyr;
                int[] yyd;
                int[] yyal;
                int[] yyah;
                int[] yygl;
                int[] yygh;

                int yyn = 0;
                int yystate = 0;
                int yychar = -1;
                int yynerrs = 0;
                int yyerrflag = 0;
                int yysp = 0;
                int yymaxdepth = 4096;
                int yyflag = 0;
                int yyfnone   = 0;
                int[] yys = new int[4096];
                string[] yyv = new string[4096];

                string yyval = "";

                FileStream OutputStream;
                StreamWriter Output;

                class YYARec
                {
                        public int sym;
                        public int act;
                        public YYARec (int s, int a){ sym = s; act = a; }
                }

                class YYRRec
                {
                        public int len;
                        public int sym;
                        public YYRRec (int l, int s){ sym = s; len = l; }
                }

                ////////////////////////////////////////////////////////////////
                /// Constant values / tokens
                ////////////////////////////////////////////////////////////////
                int t_IFDEF = 257;
                int t_Char59 = 258;
                int t_IFNDEF = 259;
                int t_IFELSE = 260;
                int t_ENDIF = 261;
                int t_DEFINE = 262;
                int t_Char44 = 263;
                int t_UNDEF = 264;
                int t_INCLUDE = 265;
                int t_Char123 = 266;
                int t_Char125 = 267;
                int t_Char58 = 268;
                int t_Char124Char124 = 269;
                int t_Char38Char38 = 270;
                int t_Char124 = 271;
                int t_Char94 = 272;
                int t_Char38 = 273;
                int t_Char40 = 274;
                int t_Char41 = 275;
                int t_Char33Char61 = 276;
                int t_Char61Char61 = 277;
                int t_Char60 = 278;
                int t_Char60Char61 = 279;
                int t_Char62 = 280;
                int t_Char62Char61 = 281;
                int t_Char43 = 282;
                int t_Char45 = 283;
                int t_Char37 = 284;
                int t_Char42 = 285;
                int t_Char47 = 286;
                int t_Char33 = 287;
                int t_RULE = 288;
                int t_Char42Char61 = 289;
                int t_Char43Char61 = 290;
                int t_Char45Char61 = 291;
                int t_Char47Char61 = 292;
                int t_Char61 = 293;
                int t_ELSE = 294;
                int t_IF = 295;
                int t_WHILE = 296;
                int t_Char46 = 297;
                int t_NULL = 298;
                int t_event = 299;
                int t_global = 300;
                int t_asset = 301;
                int t_object = 302;
                int t_function = 303;
                int t_math = 304;
                int t_flag = 305;
                int t_property = 306;
                int t_command = 307;
                int t_list = 308;
                int t_skill = 309;
                int t_synonym = 310;
                int t_ambigChar95globalChar95property = 311;
                int t_ambigChar95eventChar95property = 312;
                int t_ambigChar95objectChar95flag = 313;
                int t_ambigChar95mathChar95command = 314;
                int t_ambigChar95mathChar95skillChar95property = 315;
                int t_ambigChar95synonymChar95flag = 316;
                int t_ambigChar95skillChar95property = 317;
                int t_ambigChar95commandChar95flag = 318;
                int t_ambigChar95globalChar95synonymChar95property = 319;
                int t_ambigChar95commandChar95property = 320;
                int t_integer = 321;
                int t_fixed = 322;
                int t_identifier = 323;
                int t_file = 324;
                int t_string = 325;
                int t_ignore = 256;
///////////////////////////////////////////////////////////
/// Global settings: 
///////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////


                /// <summary>
                /// Der Haupteinstiegspunkt für die Anwendung.
                /// </summary>
                [STAThread]
                static int Main(string[] args)
                {

                        bool ShowTokens = false;
                        string InputFilename = "";
                        string OutputFilename = "";

                        foreach (string s in args)
                        {
                                if (s.ToLower() == "-t")
                                {
                                        ShowTokens = true;
                                }
                                else
                                {
                                        if (InputFilename == "")  InputFilename = s; else
                                        if (OutputFilename == "") OutputFilename = s; else
                                        {
                                                Console.WriteLine("Too many arguments!");
                                                return 1;
                                        }
                                }
                        }
                        if (InputFilename == "")
                        {
                                System.Console.WriteLine("You need to specify input and outputfile: compiler.exe input.txt output.txt");
                                return 1;
                        }

                        StreamReader in_s = File.OpenText(InputFilename);
                        string inputstream = in_s.ReadToEnd();
                        in_s.Close();

                        ////////////////////////////////////////////////////////////////
                        /// Compiler Code:
                        ////////////////////////////////////////////////////////////////
                        MyCompiler compiler = new MyCompiler();
                        compiler.Output = null;
                        if (OutputFilename != "")
                        {
                                File.Delete(OutputFilename);
                                compiler.OutputStream = File.OpenWrite(OutputFilename);
                                compiler.Output = new StreamWriter(compiler.OutputStream,new System.Text.UTF8Encoding(false));
                        }
                        else
                        {
                                compiler.Output = new StreamWriter(Console.OpenStandardOutput(),new System.Text.UTF8Encoding(false));

                        }

                        if (!compiler.Scanner(inputstream)) return 1;
                        if (ShowTokens)
                        {
                                foreach (AToken t in compiler.TokenList)
                                {
                                        Console.WriteLine("TokenID: "+t.token+"  =  "+t.val);
                                }
                        }
                        compiler.InitTables();
                        if (!compiler.yyparse()) return 1;

                        if (compiler.Output != null) compiler.Output.Close();
			return 0;
                }
                public void yyaction (int yyruleno)
                {
                        switch (yyruleno)
                        {
                                ////////////////////////////////////////////////////////////////
                                /// YYAction code:
                                ////////////////////////////////////////////////////////////////
							case    1 : 
         yyval = yyv[yysp-0];
         //Output.WriteLine(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    3 : 
         yyval = "";
         
       break;
							case    4 : 
         yyval = yyv[yysp-0];
         
       break;
							case    5 : 
         yyval = yyv[yysp-0];
         
       break;
							case    6 : 
         yyval = yyv[yysp-0];
         
       break;
							case    7 : 
         yyval = yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = yyv[yysp-0];
         
       break;
							case   10 : 
         yyval = yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   12 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   13 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   14 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   15 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   16 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   17 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   18 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   19 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   20 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   21 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   22 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   23 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   24 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   28 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   29 : 
         yyval = yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = yyv[yysp-0];
         
       break;
							case   32 : 
         yyval = yyv[yysp-0];
         
       break;
							case   33 : 
         yyval = yyv[yysp-0];
         
       break;
							case   34 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   35 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   36 : 
         yyval = yyv[yysp-0];
         
       break;
							case   37 : 
         yyval = yyv[yysp-0];
         
       break;
							case   38 : 
         yyval = yyv[yysp-0];
         
       break;
							case   39 : 
         yyval = yyv[yysp-0];
         
       break;
							case   40 : 
         yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   41 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   42 : 
         yyval = yyv[yysp-0];
         
       break;
							case   43 : 
         yyval = "";
         
       break;
							case   44 : 
         yyval = yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   55 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = "";
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   78 : 
         yyval = yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = "";
         
       break;
							case   85 : 
         yyval = yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   87 : 
         yyval = yyv[yysp-0];
         
       break;
							case   88 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   89 : 
         yyval = yyv[yysp-0];
         
       break;
							case   90 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-0];
         
       break;
							case  120 : 
         yyval = yyv[yysp-0];
         
       break;
							case  121 : 
         yyval = yyv[yysp-0];
         
       break;
							case  122 : 
         yyval = yyv[yysp-0];
         
       break;
							case  123 : 
         yyval = yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  126 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  127 : 
         yyval = yyv[yysp-0];
         
       break;
							case  128 : 
         yyval = yyv[yysp-0];
         
       break;
							case  129 : 
         yyval = yyv[yysp-0];
         
       break;
							case  130 : 
         yyval = yyv[yysp-0];
         
       break;
							case  131 : 
         yyval = yyv[yysp-0];
         
       break;
							case  132 : 
         yyval = yyv[yysp-0];
         
       break;
							case  133 : 
         yyval = yyv[yysp-0];
         
       break;
							case  134 : 
         yyval = yyv[yysp-0];
         
       break;
							case  135 : 
         yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  136 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  137 : 
         yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  138 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  139 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  140 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  141 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  142 : 
         yyval = yyv[yysp-0];
         
       break;
							case  143 : 
         yyval = yyv[yysp-0];
         
       break;
							case  144 : 
         yyval = yyv[yysp-0];
         
       break;
							case  145 : 
         yyval = yyv[yysp-0];
         
       break;
							case  146 : 
         yyval = yyv[yysp-0];
         
       break;
							case  147 : 
         yyval = yyv[yysp-0];
         
       break;
							case  148 : 
         yyval = yyv[yysp-0];
         
       break;
							case  149 : 
         yyval = yyv[yysp-0];
         
       break;
							case  150 : 
         yyval = yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = yyv[yysp-0];
         
       break;
							case  158 : 
         yyval = yyv[yysp-0];
         
       break;
							case  159 : 
         yyval = yyv[yysp-0];
         
       break;
							case  160 : 
         yyval = yyv[yysp-0];
         
       break;
							case  161 : 
         yyval = yyv[yysp-0];
         
       break;
							case  162 : 
         yyval = yyv[yysp-0];
         
       break;
							case  163 : 
         yyval = yyv[yysp-0];
         
       break;
							case  164 : 
         yyval = yyv[yysp-0];
         
       break;
							case  165 : 
         yyval = yyv[yysp-0];
         
       break;
							case  166 : 
         yyval = yyv[yysp-0];
         
       break;
							case  167 : 
         yyval = yyv[yysp-0];
         
       break;
							case  168 : 
         yyval = yyv[yysp-0];
         
       break;
							case  169 : 
         yyval = yyv[yysp-0];
         
       break;
							case  170 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  171 : 
         yyval = yyv[yysp-0];
         
       break;
							case  172 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  173 : 
         yyval = yyv[yysp-0];
         
       break;
							case  174 : 
         yyval = yyv[yysp-0];
         
       break;
							case  175 : 
         yyval = yyv[yysp-0];
         
       break;
							case  176 : 
         yyval = yyv[yysp-0];
         
       break;
							case  177 : 
         yyval = yyv[yysp-0];
         
       break;
							case  178 : 
         yyval = yyv[yysp-0];
         
       break;
							case  179 : 
         yyval = yyv[yysp-0];
         
       break;
							case  180 : 
         yyval = yyv[yysp-0];
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = yyv[yysp-0];
         
       break;
							case  183 : 
         yyval = yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = yyv[yysp-0];
         
       break;
							case  187 : 
         yyval = yyv[yysp-0];
         
       break;
							case  188 : 
         yyval = yyv[yysp-0];
         
       break;
							case  189 : 
         yyval = yyv[yysp-0];
         
       break;
							case  190 : 
         yyval = yyv[yysp-0];
         
       break;
							case  191 : 
         yyval = yyv[yysp-0];
         
       break;
							case  192 : 
         yyval = yyv[yysp-0];
         
       break;
							case  193 : 
         yyval = yyv[yysp-0];
         
       break;
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 1944;
					int yyngotos  = 854;
					int yynstates = 310;
					int yynrules  = 193;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,51);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(322,64);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(266,-84 );yyac++; 
					yya[yyac] = new YYARec(258,79);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(266,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(321,81);yyac++; 
					yya[yyac] = new YYARec(322,82);yyac++; 
					yya[yyac] = new YYARec(258,83);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(258,85);yyac++; 
					yya[yyac] = new YYARec(258,86);yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(258,88);yyac++; 
					yya[yyac] = new YYARec(263,89);yyac++; 
					yya[yyac] = new YYARec(258,90);yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(266,92);yyac++; 
					yya[yyac] = new YYARec(266,94);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(258,-34 );yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(308,111);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(322,64);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(258,133);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(267,-53 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(258,150);yyac++; 
					yya[yyac] = new YYARec(260,151);yyac++; 
					yya[yyac] = new YYARec(261,152);yyac++; 
					yya[yyac] = new YYARec(258,153);yyac++; 
					yya[yyac] = new YYARec(297,154);yyac++; 
					yya[yyac] = new YYARec(258,-146 );yyac++; 
					yya[yyac] = new YYARec(263,-146 );yyac++; 
					yya[yyac] = new YYARec(266,-146 );yyac++; 
					yya[yyac] = new YYARec(269,-146 );yyac++; 
					yya[yyac] = new YYARec(270,-146 );yyac++; 
					yya[yyac] = new YYARec(271,-146 );yyac++; 
					yya[yyac] = new YYARec(272,-146 );yyac++; 
					yya[yyac] = new YYARec(273,-146 );yyac++; 
					yya[yyac] = new YYARec(275,-146 );yyac++; 
					yya[yyac] = new YYARec(276,-146 );yyac++; 
					yya[yyac] = new YYARec(277,-146 );yyac++; 
					yya[yyac] = new YYARec(278,-146 );yyac++; 
					yya[yyac] = new YYARec(279,-146 );yyac++; 
					yya[yyac] = new YYARec(280,-146 );yyac++; 
					yya[yyac] = new YYARec(281,-146 );yyac++; 
					yya[yyac] = new YYARec(282,-146 );yyac++; 
					yya[yyac] = new YYARec(283,-146 );yyac++; 
					yya[yyac] = new YYARec(284,-146 );yyac++; 
					yya[yyac] = new YYARec(285,-146 );yyac++; 
					yya[yyac] = new YYARec(286,-146 );yyac++; 
					yya[yyac] = new YYARec(287,-146 );yyac++; 
					yya[yyac] = new YYARec(289,-146 );yyac++; 
					yya[yyac] = new YYARec(290,-146 );yyac++; 
					yya[yyac] = new YYARec(291,-146 );yyac++; 
					yya[yyac] = new YYARec(292,-146 );yyac++; 
					yya[yyac] = new YYARec(293,-146 );yyac++; 
					yya[yyac] = new YYARec(298,-146 );yyac++; 
					yya[yyac] = new YYARec(299,-146 );yyac++; 
					yya[yyac] = new YYARec(300,-146 );yyac++; 
					yya[yyac] = new YYARec(301,-146 );yyac++; 
					yya[yyac] = new YYARec(302,-146 );yyac++; 
					yya[yyac] = new YYARec(304,-146 );yyac++; 
					yya[yyac] = new YYARec(305,-146 );yyac++; 
					yya[yyac] = new YYARec(306,-146 );yyac++; 
					yya[yyac] = new YYARec(307,-146 );yyac++; 
					yya[yyac] = new YYARec(308,-146 );yyac++; 
					yya[yyac] = new YYARec(309,-146 );yyac++; 
					yya[yyac] = new YYARec(310,-146 );yyac++; 
					yya[yyac] = new YYARec(312,-146 );yyac++; 
					yya[yyac] = new YYARec(313,-146 );yyac++; 
					yya[yyac] = new YYARec(314,-146 );yyac++; 
					yya[yyac] = new YYARec(315,-146 );yyac++; 
					yya[yyac] = new YYARec(316,-146 );yyac++; 
					yya[yyac] = new YYARec(317,-146 );yyac++; 
					yya[yyac] = new YYARec(318,-146 );yyac++; 
					yya[yyac] = new YYARec(319,-146 );yyac++; 
					yya[yyac] = new YYARec(320,-146 );yyac++; 
					yya[yyac] = new YYARec(321,-146 );yyac++; 
					yya[yyac] = new YYARec(322,-146 );yyac++; 
					yya[yyac] = new YYARec(323,-146 );yyac++; 
					yya[yyac] = new YYARec(324,-146 );yyac++; 
					yya[yyac] = new YYARec(325,-146 );yyac++; 
					yya[yyac] = new YYARec(297,155);yyac++; 
					yya[yyac] = new YYARec(258,-145 );yyac++; 
					yya[yyac] = new YYARec(263,-145 );yyac++; 
					yya[yyac] = new YYARec(266,-145 );yyac++; 
					yya[yyac] = new YYARec(269,-145 );yyac++; 
					yya[yyac] = new YYARec(270,-145 );yyac++; 
					yya[yyac] = new YYARec(271,-145 );yyac++; 
					yya[yyac] = new YYARec(272,-145 );yyac++; 
					yya[yyac] = new YYARec(273,-145 );yyac++; 
					yya[yyac] = new YYARec(275,-145 );yyac++; 
					yya[yyac] = new YYARec(276,-145 );yyac++; 
					yya[yyac] = new YYARec(277,-145 );yyac++; 
					yya[yyac] = new YYARec(278,-145 );yyac++; 
					yya[yyac] = new YYARec(279,-145 );yyac++; 
					yya[yyac] = new YYARec(280,-145 );yyac++; 
					yya[yyac] = new YYARec(281,-145 );yyac++; 
					yya[yyac] = new YYARec(282,-145 );yyac++; 
					yya[yyac] = new YYARec(283,-145 );yyac++; 
					yya[yyac] = new YYARec(284,-145 );yyac++; 
					yya[yyac] = new YYARec(285,-145 );yyac++; 
					yya[yyac] = new YYARec(286,-145 );yyac++; 
					yya[yyac] = new YYARec(287,-145 );yyac++; 
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(291,-145 );yyac++; 
					yya[yyac] = new YYARec(292,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(298,-145 );yyac++; 
					yya[yyac] = new YYARec(299,-145 );yyac++; 
					yya[yyac] = new YYARec(300,-145 );yyac++; 
					yya[yyac] = new YYARec(301,-145 );yyac++; 
					yya[yyac] = new YYARec(302,-145 );yyac++; 
					yya[yyac] = new YYARec(304,-145 );yyac++; 
					yya[yyac] = new YYARec(305,-145 );yyac++; 
					yya[yyac] = new YYARec(306,-145 );yyac++; 
					yya[yyac] = new YYARec(307,-145 );yyac++; 
					yya[yyac] = new YYARec(308,-145 );yyac++; 
					yya[yyac] = new YYARec(309,-145 );yyac++; 
					yya[yyac] = new YYARec(310,-145 );yyac++; 
					yya[yyac] = new YYARec(312,-145 );yyac++; 
					yya[yyac] = new YYARec(313,-145 );yyac++; 
					yya[yyac] = new YYARec(314,-145 );yyac++; 
					yya[yyac] = new YYARec(315,-145 );yyac++; 
					yya[yyac] = new YYARec(316,-145 );yyac++; 
					yya[yyac] = new YYARec(317,-145 );yyac++; 
					yya[yyac] = new YYARec(318,-145 );yyac++; 
					yya[yyac] = new YYARec(319,-145 );yyac++; 
					yya[yyac] = new YYARec(320,-145 );yyac++; 
					yya[yyac] = new YYARec(321,-145 );yyac++; 
					yya[yyac] = new YYARec(322,-145 );yyac++; 
					yya[yyac] = new YYARec(323,-145 );yyac++; 
					yya[yyac] = new YYARec(324,-145 );yyac++; 
					yya[yyac] = new YYARec(325,-145 );yyac++; 
					yya[yyac] = new YYARec(258,156);yyac++; 
					yya[yyac] = new YYARec(258,-162 );yyac++; 
					yya[yyac] = new YYARec(263,-162 );yyac++; 
					yya[yyac] = new YYARec(282,-162 );yyac++; 
					yya[yyac] = new YYARec(283,-162 );yyac++; 
					yya[yyac] = new YYARec(287,-162 );yyac++; 
					yya[yyac] = new YYARec(298,-162 );yyac++; 
					yya[yyac] = new YYARec(299,-162 );yyac++; 
					yya[yyac] = new YYARec(300,-162 );yyac++; 
					yya[yyac] = new YYARec(301,-162 );yyac++; 
					yya[yyac] = new YYARec(302,-162 );yyac++; 
					yya[yyac] = new YYARec(304,-162 );yyac++; 
					yya[yyac] = new YYARec(305,-162 );yyac++; 
					yya[yyac] = new YYARec(306,-162 );yyac++; 
					yya[yyac] = new YYARec(307,-162 );yyac++; 
					yya[yyac] = new YYARec(308,-162 );yyac++; 
					yya[yyac] = new YYARec(309,-162 );yyac++; 
					yya[yyac] = new YYARec(310,-162 );yyac++; 
					yya[yyac] = new YYARec(312,-162 );yyac++; 
					yya[yyac] = new YYARec(313,-162 );yyac++; 
					yya[yyac] = new YYARec(314,-162 );yyac++; 
					yya[yyac] = new YYARec(315,-162 );yyac++; 
					yya[yyac] = new YYARec(316,-162 );yyac++; 
					yya[yyac] = new YYARec(317,-162 );yyac++; 
					yya[yyac] = new YYARec(318,-162 );yyac++; 
					yya[yyac] = new YYARec(319,-162 );yyac++; 
					yya[yyac] = new YYARec(320,-162 );yyac++; 
					yya[yyac] = new YYARec(321,-162 );yyac++; 
					yya[yyac] = new YYARec(322,-162 );yyac++; 
					yya[yyac] = new YYARec(323,-162 );yyac++; 
					yya[yyac] = new YYARec(324,-162 );yyac++; 
					yya[yyac] = new YYARec(325,-162 );yyac++; 
					yya[yyac] = new YYARec(268,-187 );yyac++; 
					yya[yyac] = new YYARec(289,-187 );yyac++; 
					yya[yyac] = new YYARec(290,-187 );yyac++; 
					yya[yyac] = new YYARec(291,-187 );yyac++; 
					yya[yyac] = new YYARec(292,-187 );yyac++; 
					yya[yyac] = new YYARec(293,-187 );yyac++; 
					yya[yyac] = new YYARec(297,-187 );yyac++; 
					yya[yyac] = new YYARec(289,158);yyac++; 
					yya[yyac] = new YYARec(290,159);yyac++; 
					yya[yyac] = new YYARec(291,160);yyac++; 
					yya[yyac] = new YYARec(292,161);yyac++; 
					yya[yyac] = new YYARec(293,162);yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(299,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(308,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(310,-84 );yyac++; 
					yya[yyac] = new YYARec(312,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(316,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(319,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(258,165);yyac++; 
					yya[yyac] = new YYARec(268,166);yyac++; 
					yya[yyac] = new YYARec(297,155);yyac++; 
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(291,-145 );yyac++; 
					yya[yyac] = new YYARec(292,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(268,-191 );yyac++; 
					yya[yyac] = new YYARec(267,167);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(266,191);yyac++; 
					yya[yyac] = new YYARec(258,-160 );yyac++; 
					yya[yyac] = new YYARec(263,-160 );yyac++; 
					yya[yyac] = new YYARec(282,-160 );yyac++; 
					yya[yyac] = new YYARec(283,-160 );yyac++; 
					yya[yyac] = new YYARec(287,-160 );yyac++; 
					yya[yyac] = new YYARec(298,-160 );yyac++; 
					yya[yyac] = new YYARec(299,-160 );yyac++; 
					yya[yyac] = new YYARec(300,-160 );yyac++; 
					yya[yyac] = new YYARec(301,-160 );yyac++; 
					yya[yyac] = new YYARec(302,-160 );yyac++; 
					yya[yyac] = new YYARec(304,-160 );yyac++; 
					yya[yyac] = new YYARec(305,-160 );yyac++; 
					yya[yyac] = new YYARec(306,-160 );yyac++; 
					yya[yyac] = new YYARec(307,-160 );yyac++; 
					yya[yyac] = new YYARec(308,-160 );yyac++; 
					yya[yyac] = new YYARec(309,-160 );yyac++; 
					yya[yyac] = new YYARec(310,-160 );yyac++; 
					yya[yyac] = new YYARec(312,-160 );yyac++; 
					yya[yyac] = new YYARec(313,-160 );yyac++; 
					yya[yyac] = new YYARec(314,-160 );yyac++; 
					yya[yyac] = new YYARec(315,-160 );yyac++; 
					yya[yyac] = new YYARec(316,-160 );yyac++; 
					yya[yyac] = new YYARec(317,-160 );yyac++; 
					yya[yyac] = new YYARec(318,-160 );yyac++; 
					yya[yyac] = new YYARec(319,-160 );yyac++; 
					yya[yyac] = new YYARec(320,-160 );yyac++; 
					yya[yyac] = new YYARec(321,-160 );yyac++; 
					yya[yyac] = new YYARec(322,-160 );yyac++; 
					yya[yyac] = new YYARec(323,-160 );yyac++; 
					yya[yyac] = new YYARec(324,-160 );yyac++; 
					yya[yyac] = new YYARec(325,-160 );yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(268,-132 );yyac++; 
					yya[yyac] = new YYARec(289,-132 );yyac++; 
					yya[yyac] = new YYARec(290,-132 );yyac++; 
					yya[yyac] = new YYARec(291,-132 );yyac++; 
					yya[yyac] = new YYARec(292,-132 );yyac++; 
					yya[yyac] = new YYARec(293,-132 );yyac++; 
					yya[yyac] = new YYARec(297,-132 );yyac++; 
					yya[yyac] = new YYARec(258,-161 );yyac++; 
					yya[yyac] = new YYARec(263,-161 );yyac++; 
					yya[yyac] = new YYARec(282,-161 );yyac++; 
					yya[yyac] = new YYARec(283,-161 );yyac++; 
					yya[yyac] = new YYARec(287,-161 );yyac++; 
					yya[yyac] = new YYARec(298,-161 );yyac++; 
					yya[yyac] = new YYARec(299,-161 );yyac++; 
					yya[yyac] = new YYARec(300,-161 );yyac++; 
					yya[yyac] = new YYARec(301,-161 );yyac++; 
					yya[yyac] = new YYARec(302,-161 );yyac++; 
					yya[yyac] = new YYARec(304,-161 );yyac++; 
					yya[yyac] = new YYARec(305,-161 );yyac++; 
					yya[yyac] = new YYARec(306,-161 );yyac++; 
					yya[yyac] = new YYARec(307,-161 );yyac++; 
					yya[yyac] = new YYARec(308,-161 );yyac++; 
					yya[yyac] = new YYARec(309,-161 );yyac++; 
					yya[yyac] = new YYARec(310,-161 );yyac++; 
					yya[yyac] = new YYARec(312,-161 );yyac++; 
					yya[yyac] = new YYARec(313,-161 );yyac++; 
					yya[yyac] = new YYARec(314,-161 );yyac++; 
					yya[yyac] = new YYARec(315,-161 );yyac++; 
					yya[yyac] = new YYARec(316,-161 );yyac++; 
					yya[yyac] = new YYARec(317,-161 );yyac++; 
					yya[yyac] = new YYARec(318,-161 );yyac++; 
					yya[yyac] = new YYARec(319,-161 );yyac++; 
					yya[yyac] = new YYARec(320,-161 );yyac++; 
					yya[yyac] = new YYARec(321,-161 );yyac++; 
					yya[yyac] = new YYARec(322,-161 );yyac++; 
					yya[yyac] = new YYARec(323,-161 );yyac++; 
					yya[yyac] = new YYARec(324,-161 );yyac++; 
					yya[yyac] = new YYARec(325,-161 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(303,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(258,196);yyac++; 
					yya[yyac] = new YYARec(267,197);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(267,-53 );yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(323,65);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(258,204);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,207);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(313,208);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(316,209);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(318,210);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,207);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(313,208);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(316,209);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(318,210);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(308,111);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(322,64);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(258,218);yyac++; 
					yya[yyac] = new YYARec(258,219);yyac++; 
					yya[yyac] = new YYARec(267,220);yyac++; 
					yya[yyac] = new YYARec(274,221);yyac++; 
					yya[yyac] = new YYARec(258,-189 );yyac++; 
					yya[yyac] = new YYARec(266,-189 );yyac++; 
					yya[yyac] = new YYARec(269,-189 );yyac++; 
					yya[yyac] = new YYARec(270,-189 );yyac++; 
					yya[yyac] = new YYARec(271,-189 );yyac++; 
					yya[yyac] = new YYARec(272,-189 );yyac++; 
					yya[yyac] = new YYARec(273,-189 );yyac++; 
					yya[yyac] = new YYARec(275,-189 );yyac++; 
					yya[yyac] = new YYARec(276,-189 );yyac++; 
					yya[yyac] = new YYARec(277,-189 );yyac++; 
					yya[yyac] = new YYARec(278,-189 );yyac++; 
					yya[yyac] = new YYARec(279,-189 );yyac++; 
					yya[yyac] = new YYARec(280,-189 );yyac++; 
					yya[yyac] = new YYARec(281,-189 );yyac++; 
					yya[yyac] = new YYARec(282,-189 );yyac++; 
					yya[yyac] = new YYARec(283,-189 );yyac++; 
					yya[yyac] = new YYARec(284,-189 );yyac++; 
					yya[yyac] = new YYARec(285,-189 );yyac++; 
					yya[yyac] = new YYARec(286,-189 );yyac++; 
					yya[yyac] = new YYARec(289,-189 );yyac++; 
					yya[yyac] = new YYARec(290,-189 );yyac++; 
					yya[yyac] = new YYARec(291,-189 );yyac++; 
					yya[yyac] = new YYARec(292,-189 );yyac++; 
					yya[yyac] = new YYARec(293,-189 );yyac++; 
					yya[yyac] = new YYARec(297,-189 );yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(284,224);yyac++; 
					yya[yyac] = new YYARec(285,225);yyac++; 
					yya[yyac] = new YYARec(286,226);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(270,-100 );yyac++; 
					yya[yyac] = new YYARec(271,-100 );yyac++; 
					yya[yyac] = new YYARec(272,-100 );yyac++; 
					yya[yyac] = new YYARec(273,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(276,-100 );yyac++; 
					yya[yyac] = new YYARec(277,-100 );yyac++; 
					yya[yyac] = new YYARec(278,-100 );yyac++; 
					yya[yyac] = new YYARec(279,-100 );yyac++; 
					yya[yyac] = new YYARec(280,-100 );yyac++; 
					yya[yyac] = new YYARec(281,-100 );yyac++; 
					yya[yyac] = new YYARec(282,-100 );yyac++; 
					yya[yyac] = new YYARec(283,-100 );yyac++; 
					yya[yyac] = new YYARec(282,228);yyac++; 
					yya[yyac] = new YYARec(283,229);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(269,-98 );yyac++; 
					yya[yyac] = new YYARec(270,-98 );yyac++; 
					yya[yyac] = new YYARec(271,-98 );yyac++; 
					yya[yyac] = new YYARec(272,-98 );yyac++; 
					yya[yyac] = new YYARec(273,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(276,-98 );yyac++; 
					yya[yyac] = new YYARec(277,-98 );yyac++; 
					yya[yyac] = new YYARec(278,-98 );yyac++; 
					yya[yyac] = new YYARec(279,-98 );yyac++; 
					yya[yyac] = new YYARec(280,-98 );yyac++; 
					yya[yyac] = new YYARec(281,-98 );yyac++; 
					yya[yyac] = new YYARec(278,231);yyac++; 
					yya[yyac] = new YYARec(279,232);yyac++; 
					yya[yyac] = new YYARec(280,233);yyac++; 
					yya[yyac] = new YYARec(281,234);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(266,-96 );yyac++; 
					yya[yyac] = new YYARec(269,-96 );yyac++; 
					yya[yyac] = new YYARec(270,-96 );yyac++; 
					yya[yyac] = new YYARec(271,-96 );yyac++; 
					yya[yyac] = new YYARec(272,-96 );yyac++; 
					yya[yyac] = new YYARec(273,-96 );yyac++; 
					yya[yyac] = new YYARec(275,-96 );yyac++; 
					yya[yyac] = new YYARec(276,-96 );yyac++; 
					yya[yyac] = new YYARec(277,-96 );yyac++; 
					yya[yyac] = new YYARec(276,236);yyac++; 
					yya[yyac] = new YYARec(277,237);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(266,-95 );yyac++; 
					yya[yyac] = new YYARec(269,-95 );yyac++; 
					yya[yyac] = new YYARec(270,-95 );yyac++; 
					yya[yyac] = new YYARec(271,-95 );yyac++; 
					yya[yyac] = new YYARec(272,-95 );yyac++; 
					yya[yyac] = new YYARec(273,-95 );yyac++; 
					yya[yyac] = new YYARec(275,-95 );yyac++; 
					yya[yyac] = new YYARec(273,238);yyac++; 
					yya[yyac] = new YYARec(258,-93 );yyac++; 
					yya[yyac] = new YYARec(266,-93 );yyac++; 
					yya[yyac] = new YYARec(269,-93 );yyac++; 
					yya[yyac] = new YYARec(270,-93 );yyac++; 
					yya[yyac] = new YYARec(271,-93 );yyac++; 
					yya[yyac] = new YYARec(272,-93 );yyac++; 
					yya[yyac] = new YYARec(275,-93 );yyac++; 
					yya[yyac] = new YYARec(272,239);yyac++; 
					yya[yyac] = new YYARec(258,-91 );yyac++; 
					yya[yyac] = new YYARec(266,-91 );yyac++; 
					yya[yyac] = new YYARec(269,-91 );yyac++; 
					yya[yyac] = new YYARec(270,-91 );yyac++; 
					yya[yyac] = new YYARec(271,-91 );yyac++; 
					yya[yyac] = new YYARec(275,-91 );yyac++; 
					yya[yyac] = new YYARec(271,240);yyac++; 
					yya[yyac] = new YYARec(258,-89 );yyac++; 
					yya[yyac] = new YYARec(266,-89 );yyac++; 
					yya[yyac] = new YYARec(269,-89 );yyac++; 
					yya[yyac] = new YYARec(270,-89 );yyac++; 
					yya[yyac] = new YYARec(275,-89 );yyac++; 
					yya[yyac] = new YYARec(270,241);yyac++; 
					yya[yyac] = new YYARec(258,-87 );yyac++; 
					yya[yyac] = new YYARec(266,-87 );yyac++; 
					yya[yyac] = new YYARec(269,-87 );yyac++; 
					yya[yyac] = new YYARec(275,-87 );yyac++; 
					yya[yyac] = new YYARec(269,242);yyac++; 
					yya[yyac] = new YYARec(258,-85 );yyac++; 
					yya[yyac] = new YYARec(266,-85 );yyac++; 
					yya[yyac] = new YYARec(275,-85 );yyac++; 
					yya[yyac] = new YYARec(289,158);yyac++; 
					yya[yyac] = new YYARec(290,159);yyac++; 
					yya[yyac] = new YYARec(291,160);yyac++; 
					yya[yyac] = new YYARec(292,161);yyac++; 
					yya[yyac] = new YYARec(293,162);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
					yya[yyac] = new YYARec(276,-109 );yyac++; 
					yya[yyac] = new YYARec(277,-109 );yyac++; 
					yya[yyac] = new YYARec(278,-109 );yyac++; 
					yya[yyac] = new YYARec(279,-109 );yyac++; 
					yya[yyac] = new YYARec(280,-109 );yyac++; 
					yya[yyac] = new YYARec(281,-109 );yyac++; 
					yya[yyac] = new YYARec(282,-109 );yyac++; 
					yya[yyac] = new YYARec(283,-109 );yyac++; 
					yya[yyac] = new YYARec(284,-109 );yyac++; 
					yya[yyac] = new YYARec(285,-109 );yyac++; 
					yya[yyac] = new YYARec(286,-109 );yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(266,246);yyac++; 
					yya[yyac] = new YYARec(266,247);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,256);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(322,64);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(267,-53 );yyac++; 
					yya[yyac] = new YYARec(258,258);yyac++; 
					yya[yyac] = new YYARec(258,259);yyac++; 
					yya[yyac] = new YYARec(321,81);yyac++; 
					yya[yyac] = new YYARec(263,260);yyac++; 
					yya[yyac] = new YYARec(258,-42 );yyac++; 
					yya[yyac] = new YYARec(258,261);yyac++; 
					yya[yyac] = new YYARec(257,17);yyac++; 
					yya[yyac] = new YYARec(259,18);yyac++; 
					yya[yyac] = new YYARec(262,19);yyac++; 
					yya[yyac] = new YYARec(264,20);yyac++; 
					yya[yyac] = new YYARec(265,21);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,23);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(311,27);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(319,30);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(299,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(308,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(310,-84 );yyac++; 
					yya[yyac] = new YYARec(312,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(316,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(319,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(260,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(274,188);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,189);yyac++; 
					yya[yyac] = new YYARec(322,190);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(275,279);yyac++; 
					yya[yyac] = new YYARec(267,280);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(263,77);yyac++; 
					yya[yyac] = new YYARec(258,-84 );yyac++; 
					yya[yyac] = new YYARec(282,-84 );yyac++; 
					yya[yyac] = new YYARec(283,-84 );yyac++; 
					yya[yyac] = new YYARec(287,-84 );yyac++; 
					yya[yyac] = new YYARec(298,-84 );yyac++; 
					yya[yyac] = new YYARec(300,-84 );yyac++; 
					yya[yyac] = new YYARec(301,-84 );yyac++; 
					yya[yyac] = new YYARec(302,-84 );yyac++; 
					yya[yyac] = new YYARec(303,-84 );yyac++; 
					yya[yyac] = new YYARec(304,-84 );yyac++; 
					yya[yyac] = new YYARec(305,-84 );yyac++; 
					yya[yyac] = new YYARec(306,-84 );yyac++; 
					yya[yyac] = new YYARec(307,-84 );yyac++; 
					yya[yyac] = new YYARec(309,-84 );yyac++; 
					yya[yyac] = new YYARec(313,-84 );yyac++; 
					yya[yyac] = new YYARec(314,-84 );yyac++; 
					yya[yyac] = new YYARec(315,-84 );yyac++; 
					yya[yyac] = new YYARec(317,-84 );yyac++; 
					yya[yyac] = new YYARec(318,-84 );yyac++; 
					yya[yyac] = new YYARec(320,-84 );yyac++; 
					yya[yyac] = new YYARec(321,-84 );yyac++; 
					yya[yyac] = new YYARec(322,-84 );yyac++; 
					yya[yyac] = new YYARec(323,-84 );yyac++; 
					yya[yyac] = new YYARec(324,-84 );yyac++; 
					yya[yyac] = new YYARec(325,-84 );yyac++; 
					yya[yyac] = new YYARec(297,284);yyac++; 
					yya[yyac] = new YYARec(258,-63 );yyac++; 
					yya[yyac] = new YYARec(263,-63 );yyac++; 
					yya[yyac] = new YYARec(282,-63 );yyac++; 
					yya[yyac] = new YYARec(283,-63 );yyac++; 
					yya[yyac] = new YYARec(287,-63 );yyac++; 
					yya[yyac] = new YYARec(298,-63 );yyac++; 
					yya[yyac] = new YYARec(300,-63 );yyac++; 
					yya[yyac] = new YYARec(301,-63 );yyac++; 
					yya[yyac] = new YYARec(302,-63 );yyac++; 
					yya[yyac] = new YYARec(303,-63 );yyac++; 
					yya[yyac] = new YYARec(304,-63 );yyac++; 
					yya[yyac] = new YYARec(305,-63 );yyac++; 
					yya[yyac] = new YYARec(306,-63 );yyac++; 
					yya[yyac] = new YYARec(307,-63 );yyac++; 
					yya[yyac] = new YYARec(309,-63 );yyac++; 
					yya[yyac] = new YYARec(313,-63 );yyac++; 
					yya[yyac] = new YYARec(314,-63 );yyac++; 
					yya[yyac] = new YYARec(315,-63 );yyac++; 
					yya[yyac] = new YYARec(317,-63 );yyac++; 
					yya[yyac] = new YYARec(318,-63 );yyac++; 
					yya[yyac] = new YYARec(320,-63 );yyac++; 
					yya[yyac] = new YYARec(321,-63 );yyac++; 
					yya[yyac] = new YYARec(322,-63 );yyac++; 
					yya[yyac] = new YYARec(323,-63 );yyac++; 
					yya[yyac] = new YYARec(324,-63 );yyac++; 
					yya[yyac] = new YYARec(325,-63 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(260,-53 );yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(261,289);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(308,111);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(322,64);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(260,291);yyac++; 
					yya[yyac] = new YYARec(261,292);yyac++; 
					yya[yyac] = new YYARec(258,293);yyac++; 
					yya[yyac] = new YYARec(258,294);yyac++; 
					yya[yyac] = new YYARec(275,295);yyac++; 
					yya[yyac] = new YYARec(284,224);yyac++; 
					yya[yyac] = new YYARec(285,225);yyac++; 
					yya[yyac] = new YYARec(286,226);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(271,-101 );yyac++; 
					yya[yyac] = new YYARec(272,-101 );yyac++; 
					yya[yyac] = new YYARec(273,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(276,-101 );yyac++; 
					yya[yyac] = new YYARec(277,-101 );yyac++; 
					yya[yyac] = new YYARec(278,-101 );yyac++; 
					yya[yyac] = new YYARec(279,-101 );yyac++; 
					yya[yyac] = new YYARec(280,-101 );yyac++; 
					yya[yyac] = new YYARec(281,-101 );yyac++; 
					yya[yyac] = new YYARec(282,-101 );yyac++; 
					yya[yyac] = new YYARec(283,-101 );yyac++; 
					yya[yyac] = new YYARec(282,228);yyac++; 
					yya[yyac] = new YYARec(283,229);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(270,-99 );yyac++; 
					yya[yyac] = new YYARec(271,-99 );yyac++; 
					yya[yyac] = new YYARec(272,-99 );yyac++; 
					yya[yyac] = new YYARec(273,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(276,-99 );yyac++; 
					yya[yyac] = new YYARec(277,-99 );yyac++; 
					yya[yyac] = new YYARec(278,-99 );yyac++; 
					yya[yyac] = new YYARec(279,-99 );yyac++; 
					yya[yyac] = new YYARec(280,-99 );yyac++; 
					yya[yyac] = new YYARec(281,-99 );yyac++; 
					yya[yyac] = new YYARec(278,231);yyac++; 
					yya[yyac] = new YYARec(279,232);yyac++; 
					yya[yyac] = new YYARec(280,233);yyac++; 
					yya[yyac] = new YYARec(281,234);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(269,-97 );yyac++; 
					yya[yyac] = new YYARec(270,-97 );yyac++; 
					yya[yyac] = new YYARec(271,-97 );yyac++; 
					yya[yyac] = new YYARec(272,-97 );yyac++; 
					yya[yyac] = new YYARec(273,-97 );yyac++; 
					yya[yyac] = new YYARec(275,-97 );yyac++; 
					yya[yyac] = new YYARec(276,-97 );yyac++; 
					yya[yyac] = new YYARec(277,-97 );yyac++; 
					yya[yyac] = new YYARec(276,236);yyac++; 
					yya[yyac] = new YYARec(277,237);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(269,-94 );yyac++; 
					yya[yyac] = new YYARec(270,-94 );yyac++; 
					yya[yyac] = new YYARec(271,-94 );yyac++; 
					yya[yyac] = new YYARec(272,-94 );yyac++; 
					yya[yyac] = new YYARec(273,-94 );yyac++; 
					yya[yyac] = new YYARec(275,-94 );yyac++; 
					yya[yyac] = new YYARec(273,238);yyac++; 
					yya[yyac] = new YYARec(258,-92 );yyac++; 
					yya[yyac] = new YYARec(266,-92 );yyac++; 
					yya[yyac] = new YYARec(269,-92 );yyac++; 
					yya[yyac] = new YYARec(270,-92 );yyac++; 
					yya[yyac] = new YYARec(271,-92 );yyac++; 
					yya[yyac] = new YYARec(272,-92 );yyac++; 
					yya[yyac] = new YYARec(275,-92 );yyac++; 
					yya[yyac] = new YYARec(272,239);yyac++; 
					yya[yyac] = new YYARec(258,-90 );yyac++; 
					yya[yyac] = new YYARec(266,-90 );yyac++; 
					yya[yyac] = new YYARec(269,-90 );yyac++; 
					yya[yyac] = new YYARec(270,-90 );yyac++; 
					yya[yyac] = new YYARec(271,-90 );yyac++; 
					yya[yyac] = new YYARec(275,-90 );yyac++; 
					yya[yyac] = new YYARec(271,240);yyac++; 
					yya[yyac] = new YYARec(258,-88 );yyac++; 
					yya[yyac] = new YYARec(266,-88 );yyac++; 
					yya[yyac] = new YYARec(269,-88 );yyac++; 
					yya[yyac] = new YYARec(270,-88 );yyac++; 
					yya[yyac] = new YYARec(275,-88 );yyac++; 
					yya[yyac] = new YYARec(270,241);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(266,-86 );yyac++; 
					yya[yyac] = new YYARec(269,-86 );yyac++; 
					yya[yyac] = new YYARec(275,-86 );yyac++; 
					yya[yyac] = new YYARec(267,296);yyac++; 
					yya[yyac] = new YYARec(267,297);yyac++; 
					yya[yyac] = new YYARec(282,60);yyac++; 
					yya[yyac] = new YYARec(283,61);yyac++; 
					yya[yyac] = new YYARec(287,62);yyac++; 
					yya[yyac] = new YYARec(298,256);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,42);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(321,63);yyac++; 
					yya[yyac] = new YYARec(322,64);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(324,66);yyac++; 
					yya[yyac] = new YYARec(325,67);yyac++; 
					yya[yyac] = new YYARec(258,-55 );yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(260,300);yyac++; 
					yya[yyac] = new YYARec(261,301);yyac++; 
					yya[yyac] = new YYARec(258,302);yyac++; 
					yya[yyac] = new YYARec(258,303);yyac++; 
					yya[yyac] = new YYARec(258,304);yyac++; 
					yya[yyac] = new YYARec(258,305);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(259,126);yyac++; 
					yya[yyac] = new YYARec(266,127);yyac++; 
					yya[yyac] = new YYARec(288,128);yyac++; 
					yya[yyac] = new YYARec(294,129);yyac++; 
					yya[yyac] = new YYARec(295,130);yyac++; 
					yya[yyac] = new YYARec(296,131);yyac++; 
					yya[yyac] = new YYARec(298,110);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,36);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,37);yyac++; 
					yya[yyac] = new YYARec(305,38);yyac++; 
					yya[yyac] = new YYARec(306,39);yyac++; 
					yya[yyac] = new YYARec(307,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,112);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,132);yyac++; 
					yya[yyac] = new YYARec(315,43);yyac++; 
					yya[yyac] = new YYARec(316,113);yyac++; 
					yya[yyac] = new YYARec(317,44);yyac++; 
					yya[yyac] = new YYARec(318,45);yyac++; 
					yya[yyac] = new YYARec(319,114);yyac++; 
					yya[yyac] = new YYARec(320,46);yyac++; 
					yya[yyac] = new YYARec(323,47);yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(257,140);yyac++; 
					yya[yyac] = new YYARec(259,141);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,142);yyac++; 
					yya[yyac] = new YYARec(311,143);yyac++; 
					yya[yyac] = new YYARec(312,144);yyac++; 
					yya[yyac] = new YYARec(315,145);yyac++; 
					yya[yyac] = new YYARec(317,146);yyac++; 
					yya[yyac] = new YYARec(319,147);yyac++; 
					yya[yyac] = new YYARec(320,148);yyac++; 
					yya[yyac] = new YYARec(261,-53 );yyac++; 
					yya[yyac] = new YYARec(261,308);yyac++; 
					yya[yyac] = new YYARec(261,309);yyac++;

					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,15);yygc++; 
					yyg[yygc] = new YYARec(-2,16);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,35);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,48);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-31,49);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,50);yygc++; 
					yyg[yygc] = new YYARec(-72,52);yygc++; 
					yyg[yygc] = new YYARec(-65,53);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-29,55);yygc++; 
					yyg[yygc] = new YYARec(-28,56);yygc++; 
					yyg[yygc] = new YYARec(-25,57);yygc++; 
					yyg[yygc] = new YYARec(-21,58);yygc++; 
					yyg[yygc] = new YYARec(-12,59);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,68);yygc++; 
					yyg[yygc] = new YYARec(-23,69);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,70);yygc++; 
					yyg[yygc] = new YYARec(-12,71);yygc++; 
					yyg[yygc] = new YYARec(-12,72);yygc++; 
					yyg[yygc] = new YYARec(-12,73);yygc++; 
					yyg[yygc] = new YYARec(-12,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-27,76);yygc++; 
					yyg[yygc] = new YYARec(-27,78);yygc++; 
					yyg[yygc] = new YYARec(-27,80);yygc++; 
					yyg[yygc] = new YYARec(-27,84);yygc++; 
					yyg[yygc] = new YYARec(-28,93);yygc++; 
					yyg[yygc] = new YYARec(-21,95);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,68);yygc++; 
					yyg[yygc] = new YYARec(-23,96);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,98);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-13,99);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,98);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-72,52);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-65,53);yygc++; 
					yyg[yygc] = new YYARec(-48,101);yygc++; 
					yyg[yygc] = new YYARec(-47,102);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-29,104);yygc++; 
					yyg[yygc] = new YYARec(-28,105);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-21,108);yygc++; 
					yyg[yygc] = new YYARec(-20,109);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,123);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-37,134);yygc++; 
					yyg[yygc] = new YYARec(-36,135);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-19,138);yygc++; 
					yyg[yygc] = new YYARec(-17,139);yygc++; 
					yyg[yygc] = new YYARec(-27,149);yygc++; 
					yyg[yygc] = new YYARec(-68,157);yygc++; 
					yyg[yygc] = new YYARec(-27,163);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,164);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,168);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-12,169);yygc++; 
					yyg[yygc] = new YYARec(-12,170);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,171);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,186);yygc++; 
					yyg[yygc] = new YYARec(-48,187);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,192);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,194);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-27,195);yygc++; 
					yyg[yygc] = new YYARec(-37,134);yygc++; 
					yyg[yygc] = new YYARec(-36,135);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-19,198);yygc++; 
					yyg[yygc] = new YYARec(-17,139);yygc++; 
					yyg[yygc] = new YYARec(-12,199);yygc++; 
					yyg[yygc] = new YYARec(-12,200);yygc++; 
					yyg[yygc] = new YYARec(-65,201);yygc++; 
					yyg[yygc] = new YYARec(-33,202);yygc++; 
					yyg[yygc] = new YYARec(-32,203);yygc++; 
					yyg[yygc] = new YYARec(-69,205);yygc++; 
					yyg[yygc] = new YYARec(-37,206);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-69,211);yygc++; 
					yyg[yygc] = new YYARec(-37,212);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,213);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-72,52);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-65,53);yygc++; 
					yyg[yygc] = new YYARec(-48,101);yygc++; 
					yyg[yygc] = new YYARec(-47,102);yygc++; 
					yyg[yygc] = new YYARec(-46,214);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-29,104);yygc++; 
					yyg[yygc] = new YYARec(-28,105);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-21,108);yygc++; 
					yyg[yygc] = new YYARec(-20,215);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,216);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,217);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,222);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,227);yygc++; 
					yyg[yygc] = new YYARec(-59,230);yygc++; 
					yyg[yygc] = new YYARec(-57,235);yygc++; 
					yyg[yygc] = new YYARec(-68,243);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,244);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,245);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-72,52);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-65,53);yygc++; 
					yyg[yygc] = new YYARec(-40,248);yygc++; 
					yyg[yygc] = new YYARec(-39,249);yygc++; 
					yyg[yygc] = new YYARec(-38,250);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,251);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-29,252);yygc++; 
					yyg[yygc] = new YYARec(-28,253);yygc++; 
					yyg[yygc] = new YYARec(-26,254);yygc++; 
					yyg[yygc] = new YYARec(-21,255);yygc++; 
					yyg[yygc] = new YYARec(-37,134);yygc++; 
					yyg[yygc] = new YYARec(-36,135);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-19,257);yygc++; 
					yyg[yygc] = new YYARec(-17,139);yygc++; 
					yyg[yygc] = new YYARec(-40,1);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,3);yygc++; 
					yyg[yygc] = new YYARec(-30,4);yygc++; 
					yyg[yygc] = new YYARec(-24,5);yygc++; 
					yyg[yygc] = new YYARec(-22,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,262);yygc++; 
					yyg[yygc] = new YYARec(-27,263);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,264);yygc++; 
					yyg[yygc] = new YYARec(-15,265);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,264);yygc++; 
					yyg[yygc] = new YYARec(-15,266);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,267);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,268);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,269);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,270);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,271);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,272);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,273);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,274);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,275);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,276);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,277);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-67,172);yygc++; 
					yyg[yygc] = new YYARec(-66,173);yygc++; 
					yyg[yygc] = new YYARec(-65,174);yygc++; 
					yyg[yygc] = new YYARec(-64,175);yygc++; 
					yyg[yygc] = new YYARec(-62,176);yygc++; 
					yyg[yygc] = new YYARec(-60,177);yygc++; 
					yyg[yygc] = new YYARec(-58,178);yygc++; 
					yyg[yygc] = new YYARec(-56,179);yygc++; 
					yyg[yygc] = new YYARec(-55,180);yygc++; 
					yyg[yygc] = new YYARec(-54,181);yygc++; 
					yyg[yygc] = new YYARec(-53,182);yygc++; 
					yyg[yygc] = new YYARec(-52,183);yygc++; 
					yyg[yygc] = new YYARec(-51,184);yygc++; 
					yyg[yygc] = new YYARec(-50,185);yygc++; 
					yyg[yygc] = new YYARec(-49,278);yygc++; 
					yyg[yygc] = new YYARec(-48,193);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,281);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,282);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-27,283);yygc++; 
					yyg[yygc] = new YYARec(-37,134);yygc++; 
					yyg[yygc] = new YYARec(-36,135);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-19,285);yygc++; 
					yyg[yygc] = new YYARec(-18,286);yygc++; 
					yyg[yygc] = new YYARec(-17,139);yygc++; 
					yyg[yygc] = new YYARec(-37,134);yygc++; 
					yyg[yygc] = new YYARec(-36,135);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-19,285);yygc++; 
					yyg[yygc] = new YYARec(-18,287);yygc++; 
					yyg[yygc] = new YYARec(-17,139);yygc++; 
					yyg[yygc] = new YYARec(-65,201);yygc++; 
					yyg[yygc] = new YYARec(-33,202);yygc++; 
					yyg[yygc] = new YYARec(-32,288);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-72,52);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-65,53);yygc++; 
					yyg[yygc] = new YYARec(-48,101);yygc++; 
					yyg[yygc] = new YYARec(-47,102);yygc++; 
					yyg[yygc] = new YYARec(-46,290);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-29,104);yygc++; 
					yyg[yygc] = new YYARec(-28,105);yygc++; 
					yyg[yygc] = new YYARec(-26,106);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-21,108);yygc++; 
					yyg[yygc] = new YYARec(-20,215);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,227);yygc++; 
					yyg[yygc] = new YYARec(-59,230);yygc++; 
					yyg[yygc] = new YYARec(-57,235);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-72,52);yygc++; 
					yyg[yygc] = new YYARec(-71,32);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-65,53);yygc++; 
					yyg[yygc] = new YYARec(-40,248);yygc++; 
					yyg[yygc] = new YYARec(-39,249);yygc++; 
					yyg[yygc] = new YYARec(-38,298);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,251);yygc++; 
					yyg[yygc] = new YYARec(-33,54);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-29,252);yygc++; 
					yyg[yygc] = new YYARec(-28,253);yygc++; 
					yyg[yygc] = new YYARec(-26,254);yygc++; 
					yyg[yygc] = new YYARec(-21,255);yygc++; 
					yyg[yygc] = new YYARec(-37,299);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-73,31);yygc++; 
					yyg[yygc] = new YYARec(-71,115);yygc++; 
					yyg[yygc] = new YYARec(-70,100);yygc++; 
					yyg[yygc] = new YYARec(-66,33);yygc++; 
					yyg[yygc] = new YYARec(-48,116);yygc++; 
					yyg[yygc] = new YYARec(-45,117);yygc++; 
					yyg[yygc] = new YYARec(-44,118);yygc++; 
					yyg[yygc] = new YYARec(-43,119);yygc++; 
					yyg[yygc] = new YYARec(-42,120);yygc++; 
					yyg[yygc] = new YYARec(-41,121);yygc++; 
					yyg[yygc] = new YYARec(-35,2);yygc++; 
					yyg[yygc] = new YYARec(-34,103);yygc++; 
					yyg[yygc] = new YYARec(-30,34);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-22,107);yygc++; 
					yyg[yygc] = new YYARec(-16,306);yygc++; 
					yyg[yygc] = new YYARec(-14,124);yygc++; 
					yyg[yygc] = new YYARec(-37,134);yygc++; 
					yyg[yygc] = new YYARec(-36,135);yygc++; 
					yyg[yygc] = new YYARec(-35,136);yygc++; 
					yyg[yygc] = new YYARec(-30,137);yygc++; 
					yyg[yygc] = new YYARec(-19,307);yygc++; 
					yyg[yygc] = new YYARec(-17,139);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -50;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = 0;  
					yyd[7] = -10;  
					yyd[8] = -9;  
					yyd[9] = -8;  
					yyd[10] = -7;  
					yyd[11] = -6;  
					yyd[12] = -5;  
					yyd[13] = -4;  
					yyd[14] = 0;  
					yyd[15] = -1;  
					yyd[16] = 0;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = 0;  
					yyd[20] = 0;  
					yyd[21] = 0;  
					yyd[22] = -33;  
					yyd[23] = -31;  
					yyd[24] = -44;  
					yyd[25] = -48;  
					yyd[26] = -66;  
					yyd[27] = -29;  
					yyd[28] = -32;  
					yyd[29] = -49;  
					yyd[30] = -30;  
					yyd[31] = -190;  
					yyd[32] = -187;  
					yyd[33] = -189;  
					yyd[34] = -188;  
					yyd[35] = 0;  
					yyd[36] = -184;  
					yyd[37] = -134;  
					yyd[38] = -183;  
					yyd[39] = -186;  
					yyd[40] = -165;  
					yyd[41] = -179;  
					yyd[42] = -132;  
					yyd[43] = -133;  
					yyd[44] = -178;  
					yyd[45] = -163;  
					yyd[46] = -164;  
					yyd[47] = -185;  
					yyd[48] = 0;  
					yyd[49] = 0;  
					yyd[50] = -177;  
					yyd[51] = -176;  
					yyd[52] = -166;  
					yyd[53] = 0;  
					yyd[54] = -167;  
					yyd[55] = -38;  
					yyd[56] = -36;  
					yyd[57] = 0;  
					yyd[58] = -39;  
					yyd[59] = -37;  
					yyd[60] = -122;  
					yyd[61] = -123;  
					yyd[62] = -121;  
					yyd[63] = -171;  
					yyd[64] = -173;  
					yyd[65] = -174;  
					yyd[66] = -192;  
					yyd[67] = -193;  
					yyd[68] = 0;  
					yyd[69] = 0;  
					yyd[70] = -2;  
					yyd[71] = 0;  
					yyd[72] = 0;  
					yyd[73] = 0;  
					yyd[74] = 0;  
					yyd[75] = 0;  
					yyd[76] = 0;  
					yyd[77] = -83;  
					yyd[78] = 0;  
					yyd[79] = -47;  
					yyd[80] = 0;  
					yyd[81] = -170;  
					yyd[82] = -172;  
					yyd[83] = -28;  
					yyd[84] = 0;  
					yyd[85] = -27;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = -24;  
					yyd[89] = 0;  
					yyd[90] = -25;  
					yyd[91] = -26;  
					yyd[92] = 0;  
					yyd[93] = 0;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = -35;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = -82;  
					yyd[102] = -79;  
					yyd[103] = -144;  
					yyd[104] = -81;  
					yyd[105] = -78;  
					yyd[106] = 0;  
					yyd[107] = -143;  
					yyd[108] = -80;  
					yyd[109] = 0;  
					yyd[110] = -142;  
					yyd[111] = -175;  
					yyd[112] = -182;  
					yyd[113] = -181;  
					yyd[114] = -180;  
					yyd[115] = 0;  
					yyd[116] = 0;  
					yyd[117] = 0;  
					yyd[118] = -73;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = 0;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = -46;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = -159;  
					yyd[137] = -158;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = -157;  
					yyd[143] = -153;  
					yyd[144] = -152;  
					yyd[145] = -155;  
					yyd[146] = -156;  
					yyd[147] = -154;  
					yyd[148] = -151;  
					yyd[149] = 0;  
					yyd[150] = -11;  
					yyd[151] = 0;  
					yyd[152] = -14;  
					yyd[153] = -12;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = -23;  
					yyd[157] = 0;  
					yyd[158] = -127;  
					yyd[159] = -128;  
					yyd[160] = -129;  
					yyd[161] = -130;  
					yyd[162] = -131;  
					yyd[163] = 0;  
					yyd[164] = -71;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = -65;  
					yyd[168] = -70;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = -108;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = -104;  
					yyd[176] = -102;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = -124;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = -169;  
					yyd[190] = -168;  
					yyd[191] = 0;  
					yyd[192] = 0;  
					yyd[193] = -109;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = -45;  
					yyd[198] = -52;  
					yyd[199] = 0;  
					yyd[200] = 0;  
					yyd[201] = 0;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = -139;  
					yyd[206] = -140;  
					yyd[207] = -150;  
					yyd[208] = -148;  
					yyd[209] = -149;  
					yyd[210] = -147;  
					yyd[211] = -138;  
					yyd[212] = -141;  
					yyd[213] = -126;  
					yyd[214] = -74;  
					yyd[215] = 0;  
					yyd[216] = -69;  
					yyd[217] = -68;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = 0;  
					yyd[222] = -105;  
					yyd[223] = 0;  
					yyd[224] = -118;  
					yyd[225] = -119;  
					yyd[226] = -120;  
					yyd[227] = 0;  
					yyd[228] = -116;  
					yyd[229] = -117;  
					yyd[230] = 0;  
					yyd[231] = -112;  
					yyd[232] = -113;  
					yyd[233] = -114;  
					yyd[234] = -115;  
					yyd[235] = 0;  
					yyd[236] = -110;  
					yyd[237] = -111;  
					yyd[238] = 0;  
					yyd[239] = 0;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = 0;  
					yyd[245] = 0;  
					yyd[246] = 0;  
					yyd[247] = 0;  
					yyd[248] = -61;  
					yyd[249] = 0;  
					yyd[250] = -54;  
					yyd[251] = -64;  
					yyd[252] = -62;  
					yyd[253] = -60;  
					yyd[254] = 0;  
					yyd[255] = -59;  
					yyd[256] = -58;  
					yyd[257] = -51;  
					yyd[258] = 0;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = -40;  
					yyd[262] = 0;  
					yyd[263] = 0;  
					yyd[264] = 0;  
					yyd[265] = 0;  
					yyd[266] = 0;  
					yyd[267] = -67;  
					yyd[268] = 0;  
					yyd[269] = -103;  
					yyd[270] = 0;  
					yyd[271] = 0;  
					yyd[272] = 0;  
					yyd[273] = 0;  
					yyd[274] = 0;  
					yyd[275] = 0;  
					yyd[276] = 0;  
					yyd[277] = 0;  
					yyd[278] = -125;  
					yyd[279] = -107;  
					yyd[280] = -135;  
					yyd[281] = 0;  
					yyd[282] = 0;  
					yyd[283] = 0;  
					yyd[284] = 0;  
					yyd[285] = 0;  
					yyd[286] = 0;  
					yyd[287] = 0;  
					yyd[288] = -41;  
					yyd[289] = -13;  
					yyd[290] = -77;  
					yyd[291] = 0;  
					yyd[292] = -18;  
					yyd[293] = -15;  
					yyd[294] = -16;  
					yyd[295] = -106;  
					yyd[296] = -136;  
					yyd[297] = -137;  
					yyd[298] = -56;  
					yyd[299] = -57;  
					yyd[300] = 0;  
					yyd[301] = -22;  
					yyd[302] = -19;  
					yyd[303] = -20;  
					yyd[304] = 0;  
					yyd[305] = 0;  
					yyd[306] = 0;  
					yyd[307] = 0;  
					yyd[308] = -17;  
					yyd[309] = -21; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 16;  
					yyal[2] = 29;  
					yyal[3] = 29;  
					yyal[4] = 42;  
					yyal[5] = 56;  
					yyal[6] = 64;  
					yyal[7] = 77;  
					yyal[8] = 77;  
					yyal[9] = 77;  
					yyal[10] = 77;  
					yyal[11] = 77;  
					yyal[12] = 77;  
					yyal[13] = 77;  
					yyal[14] = 77;  
					yyal[15] = 94;  
					yyal[16] = 94;  
					yyal[17] = 95;  
					yyal[18] = 96;  
					yyal[19] = 97;  
					yyal[20] = 98;  
					yyal[21] = 99;  
					yyal[22] = 100;  
					yyal[23] = 100;  
					yyal[24] = 100;  
					yyal[25] = 100;  
					yyal[26] = 100;  
					yyal[27] = 100;  
					yyal[28] = 100;  
					yyal[29] = 100;  
					yyal[30] = 100;  
					yyal[31] = 100;  
					yyal[32] = 100;  
					yyal[33] = 100;  
					yyal[34] = 100;  
					yyal[35] = 100;  
					yyal[36] = 102;  
					yyal[37] = 102;  
					yyal[38] = 102;  
					yyal[39] = 102;  
					yyal[40] = 102;  
					yyal[41] = 102;  
					yyal[42] = 102;  
					yyal[43] = 102;  
					yyal[44] = 102;  
					yyal[45] = 102;  
					yyal[46] = 102;  
					yyal[47] = 102;  
					yyal[48] = 102;  
					yyal[49] = 106;  
					yyal[50] = 108;  
					yyal[51] = 108;  
					yyal[52] = 108;  
					yyal[53] = 108;  
					yyal[54] = 110;  
					yyal[55] = 110;  
					yyal[56] = 110;  
					yyal[57] = 110;  
					yyal[58] = 111;  
					yyal[59] = 111;  
					yyal[60] = 111;  
					yyal[61] = 111;  
					yyal[62] = 111;  
					yyal[63] = 111;  
					yyal[64] = 111;  
					yyal[65] = 111;  
					yyal[66] = 111;  
					yyal[67] = 111;  
					yyal[68] = 111;  
					yyal[69] = 126;  
					yyal[70] = 127;  
					yyal[71] = 127;  
					yyal[72] = 128;  
					yyal[73] = 129;  
					yyal[74] = 131;  
					yyal[75] = 132;  
					yyal[76] = 133;  
					yyal[77] = 134;  
					yyal[78] = 134;  
					yyal[79] = 136;  
					yyal[80] = 136;  
					yyal[81] = 137;  
					yyal[82] = 137;  
					yyal[83] = 137;  
					yyal[84] = 137;  
					yyal[85] = 151;  
					yyal[86] = 151;  
					yyal[87] = 167;  
					yyal[88] = 183;  
					yyal[89] = 183;  
					yyal[90] = 212;  
					yyal[91] = 212;  
					yyal[92] = 212;  
					yyal[93] = 241;  
					yyal[94] = 242;  
					yyal[95] = 254;  
					yyal[96] = 260;  
					yyal[97] = 260;  
					yyal[98] = 261;  
					yyal[99] = 263;  
					yyal[100] = 264;  
					yyal[101] = 317;  
					yyal[102] = 317;  
					yyal[103] = 317;  
					yyal[104] = 317;  
					yyal[105] = 317;  
					yyal[106] = 317;  
					yyal[107] = 370;  
					yyal[108] = 370;  
					yyal[109] = 370;  
					yyal[110] = 371;  
					yyal[111] = 371;  
					yyal[112] = 371;  
					yyal[113] = 371;  
					yyal[114] = 371;  
					yyal[115] = 371;  
					yyal[116] = 409;  
					yyal[117] = 414;  
					yyal[118] = 445;  
					yyal[119] = 445;  
					yyal[120] = 476;  
					yyal[121] = 477;  
					yyal[122] = 478;  
					yyal[123] = 485;  
					yyal[124] = 486;  
					yyal[125] = 517;  
					yyal[126] = 518;  
					yyal[127] = 519;  
					yyal[128] = 548;  
					yyal[129] = 575;  
					yyal[130] = 607;  
					yyal[131] = 634;  
					yyal[132] = 661;  
					yyal[133] = 699;  
					yyal[134] = 699;  
					yyal[135] = 724;  
					yyal[136] = 725;  
					yyal[137] = 725;  
					yyal[138] = 725;  
					yyal[139] = 726;  
					yyal[140] = 740;  
					yyal[141] = 741;  
					yyal[142] = 742;  
					yyal[143] = 742;  
					yyal[144] = 742;  
					yyal[145] = 742;  
					yyal[146] = 742;  
					yyal[147] = 742;  
					yyal[148] = 742;  
					yyal[149] = 742;  
					yyal[150] = 747;  
					yyal[151] = 747;  
					yyal[152] = 748;  
					yyal[153] = 748;  
					yyal[154] = 748;  
					yyal[155] = 761;  
					yyal[156] = 774;  
					yyal[157] = 774;  
					yyal[158] = 801;  
					yyal[159] = 801;  
					yyal[160] = 801;  
					yyal[161] = 801;  
					yyal[162] = 801;  
					yyal[163] = 801;  
					yyal[164] = 830;  
					yyal[165] = 830;  
					yyal[166] = 861;  
					yyal[167] = 892;  
					yyal[168] = 892;  
					yyal[169] = 892;  
					yyal[170] = 893;  
					yyal[171] = 894;  
					yyal[172] = 895;  
					yyal[173] = 895;  
					yyal[174] = 921;  
					yyal[175] = 948;  
					yyal[176] = 948;  
					yyal[177] = 948;  
					yyal[178] = 967;  
					yyal[179] = 983;  
					yyal[180] = 997;  
					yyal[181] = 1007;  
					yyal[182] = 1015;  
					yyal[183] = 1022;  
					yyal[184] = 1028;  
					yyal[185] = 1033;  
					yyal[186] = 1037;  
					yyal[187] = 1037;  
					yyal[188] = 1059;  
					yyal[189] = 1086;  
					yyal[190] = 1086;  
					yyal[191] = 1086;  
					yyal[192] = 1115;  
					yyal[193] = 1116;  
					yyal[194] = 1116;  
					yyal[195] = 1117;  
					yyal[196] = 1141;  
					yyal[197] = 1155;  
					yyal[198] = 1155;  
					yyal[199] = 1155;  
					yyal[200] = 1156;  
					yyal[201] = 1157;  
					yyal[202] = 1158;  
					yyal[203] = 1160;  
					yyal[204] = 1161;  
					yyal[205] = 1176;  
					yyal[206] = 1176;  
					yyal[207] = 1176;  
					yyal[208] = 1176;  
					yyal[209] = 1176;  
					yyal[210] = 1176;  
					yyal[211] = 1176;  
					yyal[212] = 1176;  
					yyal[213] = 1176;  
					yyal[214] = 1176;  
					yyal[215] = 1176;  
					yyal[216] = 1207;  
					yyal[217] = 1207;  
					yyal[218] = 1207;  
					yyal[219] = 1237;  
					yyal[220] = 1267;  
					yyal[221] = 1298;  
					yyal[222] = 1325;  
					yyal[223] = 1325;  
					yyal[224] = 1352;  
					yyal[225] = 1352;  
					yyal[226] = 1352;  
					yyal[227] = 1352;  
					yyal[228] = 1379;  
					yyal[229] = 1379;  
					yyal[230] = 1379;  
					yyal[231] = 1406;  
					yyal[232] = 1406;  
					yyal[233] = 1406;  
					yyal[234] = 1406;  
					yyal[235] = 1406;  
					yyal[236] = 1433;  
					yyal[237] = 1433;  
					yyal[238] = 1433;  
					yyal[239] = 1460;  
					yyal[240] = 1487;  
					yyal[241] = 1514;  
					yyal[242] = 1541;  
					yyal[243] = 1568;  
					yyal[244] = 1595;  
					yyal[245] = 1596;  
					yyal[246] = 1597;  
					yyal[247] = 1626;  
					yyal[248] = 1655;  
					yyal[249] = 1655;  
					yyal[250] = 1681;  
					yyal[251] = 1681;  
					yyal[252] = 1681;  
					yyal[253] = 1681;  
					yyal[254] = 1681;  
					yyal[255] = 1708;  
					yyal[256] = 1708;  
					yyal[257] = 1708;  
					yyal[258] = 1708;  
					yyal[259] = 1721;  
					yyal[260] = 1734;  
					yyal[261] = 1739;  
					yyal[262] = 1739;  
					yyal[263] = 1740;  
					yyal[264] = 1770;  
					yyal[265] = 1772;  
					yyal[266] = 1773;  
					yyal[267] = 1774;  
					yyal[268] = 1774;  
					yyal[269] = 1775;  
					yyal[270] = 1775;  
					yyal[271] = 1794;  
					yyal[272] = 1810;  
					yyal[273] = 1824;  
					yyal[274] = 1834;  
					yyal[275] = 1842;  
					yyal[276] = 1849;  
					yyal[277] = 1855;  
					yyal[278] = 1860;  
					yyal[279] = 1860;  
					yyal[280] = 1860;  
					yyal[281] = 1860;  
					yyal[282] = 1861;  
					yyal[283] = 1862;  
					yyal[284] = 1887;  
					yyal[285] = 1896;  
					yyal[286] = 1898;  
					yyal[287] = 1899;  
					yyal[288] = 1900;  
					yyal[289] = 1900;  
					yyal[290] = 1900;  
					yyal[291] = 1900;  
					yyal[292] = 1901;  
					yyal[293] = 1901;  
					yyal[294] = 1901;  
					yyal[295] = 1901;  
					yyal[296] = 1901;  
					yyal[297] = 1901;  
					yyal[298] = 1901;  
					yyal[299] = 1901;  
					yyal[300] = 1901;  
					yyal[301] = 1902;  
					yyal[302] = 1902;  
					yyal[303] = 1902;  
					yyal[304] = 1902;  
					yyal[305] = 1931;  
					yyal[306] = 1943;  
					yyal[307] = 1944;  
					yyal[308] = 1945;  
					yyal[309] = 1945; 

					yyah = new int[yynstates];
					yyah[0] = 15;  
					yyah[1] = 28;  
					yyah[2] = 28;  
					yyah[3] = 41;  
					yyah[4] = 55;  
					yyah[5] = 63;  
					yyah[6] = 76;  
					yyah[7] = 76;  
					yyah[8] = 76;  
					yyah[9] = 76;  
					yyah[10] = 76;  
					yyah[11] = 76;  
					yyah[12] = 76;  
					yyah[13] = 76;  
					yyah[14] = 93;  
					yyah[15] = 93;  
					yyah[16] = 94;  
					yyah[17] = 95;  
					yyah[18] = 96;  
					yyah[19] = 97;  
					yyah[20] = 98;  
					yyah[21] = 99;  
					yyah[22] = 99;  
					yyah[23] = 99;  
					yyah[24] = 99;  
					yyah[25] = 99;  
					yyah[26] = 99;  
					yyah[27] = 99;  
					yyah[28] = 99;  
					yyah[29] = 99;  
					yyah[30] = 99;  
					yyah[31] = 99;  
					yyah[32] = 99;  
					yyah[33] = 99;  
					yyah[34] = 99;  
					yyah[35] = 101;  
					yyah[36] = 101;  
					yyah[37] = 101;  
					yyah[38] = 101;  
					yyah[39] = 101;  
					yyah[40] = 101;  
					yyah[41] = 101;  
					yyah[42] = 101;  
					yyah[43] = 101;  
					yyah[44] = 101;  
					yyah[45] = 101;  
					yyah[46] = 101;  
					yyah[47] = 101;  
					yyah[48] = 105;  
					yyah[49] = 107;  
					yyah[50] = 107;  
					yyah[51] = 107;  
					yyah[52] = 107;  
					yyah[53] = 109;  
					yyah[54] = 109;  
					yyah[55] = 109;  
					yyah[56] = 109;  
					yyah[57] = 110;  
					yyah[58] = 110;  
					yyah[59] = 110;  
					yyah[60] = 110;  
					yyah[61] = 110;  
					yyah[62] = 110;  
					yyah[63] = 110;  
					yyah[64] = 110;  
					yyah[65] = 110;  
					yyah[66] = 110;  
					yyah[67] = 110;  
					yyah[68] = 125;  
					yyah[69] = 126;  
					yyah[70] = 126;  
					yyah[71] = 127;  
					yyah[72] = 128;  
					yyah[73] = 130;  
					yyah[74] = 131;  
					yyah[75] = 132;  
					yyah[76] = 133;  
					yyah[77] = 133;  
					yyah[78] = 135;  
					yyah[79] = 135;  
					yyah[80] = 136;  
					yyah[81] = 136;  
					yyah[82] = 136;  
					yyah[83] = 136;  
					yyah[84] = 150;  
					yyah[85] = 150;  
					yyah[86] = 166;  
					yyah[87] = 182;  
					yyah[88] = 182;  
					yyah[89] = 211;  
					yyah[90] = 211;  
					yyah[91] = 211;  
					yyah[92] = 240;  
					yyah[93] = 241;  
					yyah[94] = 253;  
					yyah[95] = 259;  
					yyah[96] = 259;  
					yyah[97] = 260;  
					yyah[98] = 262;  
					yyah[99] = 263;  
					yyah[100] = 316;  
					yyah[101] = 316;  
					yyah[102] = 316;  
					yyah[103] = 316;  
					yyah[104] = 316;  
					yyah[105] = 316;  
					yyah[106] = 369;  
					yyah[107] = 369;  
					yyah[108] = 369;  
					yyah[109] = 370;  
					yyah[110] = 370;  
					yyah[111] = 370;  
					yyah[112] = 370;  
					yyah[113] = 370;  
					yyah[114] = 370;  
					yyah[115] = 408;  
					yyah[116] = 413;  
					yyah[117] = 444;  
					yyah[118] = 444;  
					yyah[119] = 475;  
					yyah[120] = 476;  
					yyah[121] = 477;  
					yyah[122] = 484;  
					yyah[123] = 485;  
					yyah[124] = 516;  
					yyah[125] = 517;  
					yyah[126] = 518;  
					yyah[127] = 547;  
					yyah[128] = 574;  
					yyah[129] = 606;  
					yyah[130] = 633;  
					yyah[131] = 660;  
					yyah[132] = 698;  
					yyah[133] = 698;  
					yyah[134] = 723;  
					yyah[135] = 724;  
					yyah[136] = 724;  
					yyah[137] = 724;  
					yyah[138] = 725;  
					yyah[139] = 739;  
					yyah[140] = 740;  
					yyah[141] = 741;  
					yyah[142] = 741;  
					yyah[143] = 741;  
					yyah[144] = 741;  
					yyah[145] = 741;  
					yyah[146] = 741;  
					yyah[147] = 741;  
					yyah[148] = 741;  
					yyah[149] = 746;  
					yyah[150] = 746;  
					yyah[151] = 747;  
					yyah[152] = 747;  
					yyah[153] = 747;  
					yyah[154] = 760;  
					yyah[155] = 773;  
					yyah[156] = 773;  
					yyah[157] = 800;  
					yyah[158] = 800;  
					yyah[159] = 800;  
					yyah[160] = 800;  
					yyah[161] = 800;  
					yyah[162] = 800;  
					yyah[163] = 829;  
					yyah[164] = 829;  
					yyah[165] = 860;  
					yyah[166] = 891;  
					yyah[167] = 891;  
					yyah[168] = 891;  
					yyah[169] = 892;  
					yyah[170] = 893;  
					yyah[171] = 894;  
					yyah[172] = 894;  
					yyah[173] = 920;  
					yyah[174] = 947;  
					yyah[175] = 947;  
					yyah[176] = 947;  
					yyah[177] = 966;  
					yyah[178] = 982;  
					yyah[179] = 996;  
					yyah[180] = 1006;  
					yyah[181] = 1014;  
					yyah[182] = 1021;  
					yyah[183] = 1027;  
					yyah[184] = 1032;  
					yyah[185] = 1036;  
					yyah[186] = 1036;  
					yyah[187] = 1058;  
					yyah[188] = 1085;  
					yyah[189] = 1085;  
					yyah[190] = 1085;  
					yyah[191] = 1114;  
					yyah[192] = 1115;  
					yyah[193] = 1115;  
					yyah[194] = 1116;  
					yyah[195] = 1140;  
					yyah[196] = 1154;  
					yyah[197] = 1154;  
					yyah[198] = 1154;  
					yyah[199] = 1155;  
					yyah[200] = 1156;  
					yyah[201] = 1157;  
					yyah[202] = 1159;  
					yyah[203] = 1160;  
					yyah[204] = 1175;  
					yyah[205] = 1175;  
					yyah[206] = 1175;  
					yyah[207] = 1175;  
					yyah[208] = 1175;  
					yyah[209] = 1175;  
					yyah[210] = 1175;  
					yyah[211] = 1175;  
					yyah[212] = 1175;  
					yyah[213] = 1175;  
					yyah[214] = 1175;  
					yyah[215] = 1206;  
					yyah[216] = 1206;  
					yyah[217] = 1206;  
					yyah[218] = 1236;  
					yyah[219] = 1266;  
					yyah[220] = 1297;  
					yyah[221] = 1324;  
					yyah[222] = 1324;  
					yyah[223] = 1351;  
					yyah[224] = 1351;  
					yyah[225] = 1351;  
					yyah[226] = 1351;  
					yyah[227] = 1378;  
					yyah[228] = 1378;  
					yyah[229] = 1378;  
					yyah[230] = 1405;  
					yyah[231] = 1405;  
					yyah[232] = 1405;  
					yyah[233] = 1405;  
					yyah[234] = 1405;  
					yyah[235] = 1432;  
					yyah[236] = 1432;  
					yyah[237] = 1432;  
					yyah[238] = 1459;  
					yyah[239] = 1486;  
					yyah[240] = 1513;  
					yyah[241] = 1540;  
					yyah[242] = 1567;  
					yyah[243] = 1594;  
					yyah[244] = 1595;  
					yyah[245] = 1596;  
					yyah[246] = 1625;  
					yyah[247] = 1654;  
					yyah[248] = 1654;  
					yyah[249] = 1680;  
					yyah[250] = 1680;  
					yyah[251] = 1680;  
					yyah[252] = 1680;  
					yyah[253] = 1680;  
					yyah[254] = 1707;  
					yyah[255] = 1707;  
					yyah[256] = 1707;  
					yyah[257] = 1707;  
					yyah[258] = 1720;  
					yyah[259] = 1733;  
					yyah[260] = 1738;  
					yyah[261] = 1738;  
					yyah[262] = 1739;  
					yyah[263] = 1769;  
					yyah[264] = 1771;  
					yyah[265] = 1772;  
					yyah[266] = 1773;  
					yyah[267] = 1773;  
					yyah[268] = 1774;  
					yyah[269] = 1774;  
					yyah[270] = 1793;  
					yyah[271] = 1809;  
					yyah[272] = 1823;  
					yyah[273] = 1833;  
					yyah[274] = 1841;  
					yyah[275] = 1848;  
					yyah[276] = 1854;  
					yyah[277] = 1859;  
					yyah[278] = 1859;  
					yyah[279] = 1859;  
					yyah[280] = 1859;  
					yyah[281] = 1860;  
					yyah[282] = 1861;  
					yyah[283] = 1886;  
					yyah[284] = 1895;  
					yyah[285] = 1897;  
					yyah[286] = 1898;  
					yyah[287] = 1899;  
					yyah[288] = 1899;  
					yyah[289] = 1899;  
					yyah[290] = 1899;  
					yyah[291] = 1900;  
					yyah[292] = 1900;  
					yyah[293] = 1900;  
					yyah[294] = 1900;  
					yyah[295] = 1900;  
					yyah[296] = 1900;  
					yyah[297] = 1900;  
					yyah[298] = 1900;  
					yyah[299] = 1900;  
					yyah[300] = 1901;  
					yyah[301] = 1901;  
					yyah[302] = 1901;  
					yyah[303] = 1901;  
					yyah[304] = 1930;  
					yyah[305] = 1942;  
					yyah[306] = 1943;  
					yyah[307] = 1944;  
					yyah[308] = 1944;  
					yyah[309] = 1944; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 17;  
					yygl[2] = 22;  
					yygl[3] = 22;  
					yygl[4] = 27;  
					yygl[5] = 33;  
					yygl[6] = 41;  
					yygl[7] = 47;  
					yygl[8] = 47;  
					yygl[9] = 47;  
					yygl[10] = 47;  
					yygl[11] = 47;  
					yygl[12] = 47;  
					yygl[13] = 47;  
					yygl[14] = 47;  
					yygl[15] = 62;  
					yygl[16] = 62;  
					yygl[17] = 62;  
					yygl[18] = 63;  
					yygl[19] = 64;  
					yygl[20] = 65;  
					yygl[21] = 66;  
					yygl[22] = 67;  
					yygl[23] = 67;  
					yygl[24] = 67;  
					yygl[25] = 67;  
					yygl[26] = 67;  
					yygl[27] = 67;  
					yygl[28] = 67;  
					yygl[29] = 67;  
					yygl[30] = 67;  
					yygl[31] = 67;  
					yygl[32] = 67;  
					yygl[33] = 67;  
					yygl[34] = 67;  
					yygl[35] = 67;  
					yygl[36] = 68;  
					yygl[37] = 68;  
					yygl[38] = 68;  
					yygl[39] = 68;  
					yygl[40] = 68;  
					yygl[41] = 68;  
					yygl[42] = 68;  
					yygl[43] = 68;  
					yygl[44] = 68;  
					yygl[45] = 68;  
					yygl[46] = 68;  
					yygl[47] = 68;  
					yygl[48] = 68;  
					yygl[49] = 69;  
					yygl[50] = 70;  
					yygl[51] = 70;  
					yygl[52] = 70;  
					yygl[53] = 70;  
					yygl[54] = 70;  
					yygl[55] = 70;  
					yygl[56] = 70;  
					yygl[57] = 70;  
					yygl[58] = 70;  
					yygl[59] = 70;  
					yygl[60] = 70;  
					yygl[61] = 70;  
					yygl[62] = 70;  
					yygl[63] = 70;  
					yygl[64] = 70;  
					yygl[65] = 70;  
					yygl[66] = 70;  
					yygl[67] = 70;  
					yygl[68] = 70;  
					yygl[69] = 71;  
					yygl[70] = 71;  
					yygl[71] = 71;  
					yygl[72] = 71;  
					yygl[73] = 71;  
					yygl[74] = 71;  
					yygl[75] = 71;  
					yygl[76] = 71;  
					yygl[77] = 71;  
					yygl[78] = 71;  
					yygl[79] = 72;  
					yygl[80] = 72;  
					yygl[81] = 73;  
					yygl[82] = 73;  
					yygl[83] = 73;  
					yygl[84] = 73;  
					yygl[85] = 79;  
					yygl[86] = 79;  
					yygl[87] = 95;  
					yygl[88] = 111;  
					yygl[89] = 111;  
					yygl[90] = 129;  
					yygl[91] = 129;  
					yygl[92] = 129;  
					yygl[93] = 146;  
					yygl[94] = 146;  
					yygl[95] = 152;  
					yygl[96] = 153;  
					yygl[97] = 153;  
					yygl[98] = 153;  
					yygl[99] = 153;  
					yygl[100] = 153;  
					yygl[101] = 153;  
					yygl[102] = 153;  
					yygl[103] = 153;  
					yygl[104] = 153;  
					yygl[105] = 153;  
					yygl[106] = 153;  
					yygl[107] = 153;  
					yygl[108] = 153;  
					yygl[109] = 153;  
					yygl[110] = 153;  
					yygl[111] = 153;  
					yygl[112] = 153;  
					yygl[113] = 153;  
					yygl[114] = 153;  
					yygl[115] = 153;  
					yygl[116] = 153;  
					yygl[117] = 154;  
					yygl[118] = 155;  
					yygl[119] = 155;  
					yygl[120] = 172;  
					yygl[121] = 172;  
					yygl[122] = 172;  
					yygl[123] = 172;  
					yygl[124] = 172;  
					yygl[125] = 189;  
					yygl[126] = 190;  
					yygl[127] = 191;  
					yygl[128] = 208;  
					yygl[129] = 232;  
					yygl[130] = 232;  
					yygl[131] = 256;  
					yygl[132] = 280;  
					yygl[133] = 280;  
					yygl[134] = 280;  
					yygl[135] = 281;  
					yygl[136] = 281;  
					yygl[137] = 281;  
					yygl[138] = 281;  
					yygl[139] = 281;  
					yygl[140] = 287;  
					yygl[141] = 288;  
					yygl[142] = 289;  
					yygl[143] = 289;  
					yygl[144] = 289;  
					yygl[145] = 289;  
					yygl[146] = 289;  
					yygl[147] = 289;  
					yygl[148] = 289;  
					yygl[149] = 289;  
					yygl[150] = 292;  
					yygl[151] = 292;  
					yygl[152] = 292;  
					yygl[153] = 292;  
					yygl[154] = 292;  
					yygl[155] = 296;  
					yygl[156] = 300;  
					yygl[157] = 300;  
					yygl[158] = 324;  
					yygl[159] = 324;  
					yygl[160] = 324;  
					yygl[161] = 324;  
					yygl[162] = 324;  
					yygl[163] = 324;  
					yygl[164] = 343;  
					yygl[165] = 343;  
					yygl[166] = 360;  
					yygl[167] = 377;  
					yygl[168] = 377;  
					yygl[169] = 377;  
					yygl[170] = 377;  
					yygl[171] = 377;  
					yygl[172] = 377;  
					yygl[173] = 377;  
					yygl[174] = 377;  
					yygl[175] = 391;  
					yygl[176] = 391;  
					yygl[177] = 391;  
					yygl[178] = 392;  
					yygl[179] = 393;  
					yygl[180] = 394;  
					yygl[181] = 395;  
					yygl[182] = 395;  
					yygl[183] = 395;  
					yygl[184] = 395;  
					yygl[185] = 395;  
					yygl[186] = 395;  
					yygl[187] = 395;  
					yygl[188] = 396;  
					yygl[189] = 420;  
					yygl[190] = 420;  
					yygl[191] = 420;  
					yygl[192] = 437;  
					yygl[193] = 437;  
					yygl[194] = 437;  
					yygl[195] = 437;  
					yygl[196] = 453;  
					yygl[197] = 459;  
					yygl[198] = 459;  
					yygl[199] = 459;  
					yygl[200] = 459;  
					yygl[201] = 459;  
					yygl[202] = 459;  
					yygl[203] = 459;  
					yygl[204] = 459;  
					yygl[205] = 474;  
					yygl[206] = 474;  
					yygl[207] = 474;  
					yygl[208] = 474;  
					yygl[209] = 474;  
					yygl[210] = 474;  
					yygl[211] = 474;  
					yygl[212] = 474;  
					yygl[213] = 474;  
					yygl[214] = 474;  
					yygl[215] = 474;  
					yygl[216] = 475;  
					yygl[217] = 475;  
					yygl[218] = 475;  
					yygl[219] = 493;  
					yygl[220] = 511;  
					yygl[221] = 528;  
					yygl[222] = 552;  
					yygl[223] = 552;  
					yygl[224] = 566;  
					yygl[225] = 566;  
					yygl[226] = 566;  
					yygl[227] = 566;  
					yygl[228] = 581;  
					yygl[229] = 581;  
					yygl[230] = 581;  
					yygl[231] = 597;  
					yygl[232] = 597;  
					yygl[233] = 597;  
					yygl[234] = 597;  
					yygl[235] = 597;  
					yygl[236] = 614;  
					yygl[237] = 614;  
					yygl[238] = 614;  
					yygl[239] = 632;  
					yygl[240] = 651;  
					yygl[241] = 671;  
					yygl[242] = 692;  
					yygl[243] = 714;  
					yygl[244] = 738;  
					yygl[245] = 738;  
					yygl[246] = 738;  
					yygl[247] = 755;  
					yygl[248] = 772;  
					yygl[249] = 772;  
					yygl[250] = 773;  
					yygl[251] = 773;  
					yygl[252] = 773;  
					yygl[253] = 773;  
					yygl[254] = 773;  
					yygl[255] = 773;  
					yygl[256] = 773;  
					yygl[257] = 773;  
					yygl[258] = 773;  
					yygl[259] = 780;  
					yygl[260] = 787;  
					yygl[261] = 790;  
					yygl[262] = 790;  
					yygl[263] = 790;  
					yygl[264] = 809;  
					yygl[265] = 809;  
					yygl[266] = 809;  
					yygl[267] = 809;  
					yygl[268] = 809;  
					yygl[269] = 809;  
					yygl[270] = 809;  
					yygl[271] = 810;  
					yygl[272] = 811;  
					yygl[273] = 812;  
					yygl[274] = 813;  
					yygl[275] = 813;  
					yygl[276] = 813;  
					yygl[277] = 813;  
					yygl[278] = 813;  
					yygl[279] = 813;  
					yygl[280] = 813;  
					yygl[281] = 813;  
					yygl[282] = 813;  
					yygl[283] = 813;  
					yygl[284] = 829;  
					yygl[285] = 832;  
					yygl[286] = 832;  
					yygl[287] = 832;  
					yygl[288] = 832;  
					yygl[289] = 832;  
					yygl[290] = 832;  
					yygl[291] = 832;  
					yygl[292] = 832;  
					yygl[293] = 832;  
					yygl[294] = 832;  
					yygl[295] = 832;  
					yygl[296] = 832;  
					yygl[297] = 832;  
					yygl[298] = 832;  
					yygl[299] = 832;  
					yygl[300] = 832;  
					yygl[301] = 832;  
					yygl[302] = 832;  
					yygl[303] = 832;  
					yygl[304] = 832;  
					yygl[305] = 849;  
					yygl[306] = 855;  
					yygl[307] = 855;  
					yygl[308] = 855;  
					yygl[309] = 855; 

					yygh = new int[yynstates];
					yygh[0] = 16;  
					yygh[1] = 21;  
					yygh[2] = 21;  
					yygh[3] = 26;  
					yygh[4] = 32;  
					yygh[5] = 40;  
					yygh[6] = 46;  
					yygh[7] = 46;  
					yygh[8] = 46;  
					yygh[9] = 46;  
					yygh[10] = 46;  
					yygh[11] = 46;  
					yygh[12] = 46;  
					yygh[13] = 46;  
					yygh[14] = 61;  
					yygh[15] = 61;  
					yygh[16] = 61;  
					yygh[17] = 62;  
					yygh[18] = 63;  
					yygh[19] = 64;  
					yygh[20] = 65;  
					yygh[21] = 66;  
					yygh[22] = 66;  
					yygh[23] = 66;  
					yygh[24] = 66;  
					yygh[25] = 66;  
					yygh[26] = 66;  
					yygh[27] = 66;  
					yygh[28] = 66;  
					yygh[29] = 66;  
					yygh[30] = 66;  
					yygh[31] = 66;  
					yygh[32] = 66;  
					yygh[33] = 66;  
					yygh[34] = 66;  
					yygh[35] = 67;  
					yygh[36] = 67;  
					yygh[37] = 67;  
					yygh[38] = 67;  
					yygh[39] = 67;  
					yygh[40] = 67;  
					yygh[41] = 67;  
					yygh[42] = 67;  
					yygh[43] = 67;  
					yygh[44] = 67;  
					yygh[45] = 67;  
					yygh[46] = 67;  
					yygh[47] = 67;  
					yygh[48] = 68;  
					yygh[49] = 69;  
					yygh[50] = 69;  
					yygh[51] = 69;  
					yygh[52] = 69;  
					yygh[53] = 69;  
					yygh[54] = 69;  
					yygh[55] = 69;  
					yygh[56] = 69;  
					yygh[57] = 69;  
					yygh[58] = 69;  
					yygh[59] = 69;  
					yygh[60] = 69;  
					yygh[61] = 69;  
					yygh[62] = 69;  
					yygh[63] = 69;  
					yygh[64] = 69;  
					yygh[65] = 69;  
					yygh[66] = 69;  
					yygh[67] = 69;  
					yygh[68] = 70;  
					yygh[69] = 70;  
					yygh[70] = 70;  
					yygh[71] = 70;  
					yygh[72] = 70;  
					yygh[73] = 70;  
					yygh[74] = 70;  
					yygh[75] = 70;  
					yygh[76] = 70;  
					yygh[77] = 70;  
					yygh[78] = 71;  
					yygh[79] = 71;  
					yygh[80] = 72;  
					yygh[81] = 72;  
					yygh[82] = 72;  
					yygh[83] = 72;  
					yygh[84] = 78;  
					yygh[85] = 78;  
					yygh[86] = 94;  
					yygh[87] = 110;  
					yygh[88] = 110;  
					yygh[89] = 128;  
					yygh[90] = 128;  
					yygh[91] = 128;  
					yygh[92] = 145;  
					yygh[93] = 145;  
					yygh[94] = 151;  
					yygh[95] = 152;  
					yygh[96] = 152;  
					yygh[97] = 152;  
					yygh[98] = 152;  
					yygh[99] = 152;  
					yygh[100] = 152;  
					yygh[101] = 152;  
					yygh[102] = 152;  
					yygh[103] = 152;  
					yygh[104] = 152;  
					yygh[105] = 152;  
					yygh[106] = 152;  
					yygh[107] = 152;  
					yygh[108] = 152;  
					yygh[109] = 152;  
					yygh[110] = 152;  
					yygh[111] = 152;  
					yygh[112] = 152;  
					yygh[113] = 152;  
					yygh[114] = 152;  
					yygh[115] = 152;  
					yygh[116] = 153;  
					yygh[117] = 154;  
					yygh[118] = 154;  
					yygh[119] = 171;  
					yygh[120] = 171;  
					yygh[121] = 171;  
					yygh[122] = 171;  
					yygh[123] = 171;  
					yygh[124] = 188;  
					yygh[125] = 189;  
					yygh[126] = 190;  
					yygh[127] = 207;  
					yygh[128] = 231;  
					yygh[129] = 231;  
					yygh[130] = 255;  
					yygh[131] = 279;  
					yygh[132] = 279;  
					yygh[133] = 279;  
					yygh[134] = 280;  
					yygh[135] = 280;  
					yygh[136] = 280;  
					yygh[137] = 280;  
					yygh[138] = 280;  
					yygh[139] = 286;  
					yygh[140] = 287;  
					yygh[141] = 288;  
					yygh[142] = 288;  
					yygh[143] = 288;  
					yygh[144] = 288;  
					yygh[145] = 288;  
					yygh[146] = 288;  
					yygh[147] = 288;  
					yygh[148] = 288;  
					yygh[149] = 291;  
					yygh[150] = 291;  
					yygh[151] = 291;  
					yygh[152] = 291;  
					yygh[153] = 291;  
					yygh[154] = 295;  
					yygh[155] = 299;  
					yygh[156] = 299;  
					yygh[157] = 323;  
					yygh[158] = 323;  
					yygh[159] = 323;  
					yygh[160] = 323;  
					yygh[161] = 323;  
					yygh[162] = 323;  
					yygh[163] = 342;  
					yygh[164] = 342;  
					yygh[165] = 359;  
					yygh[166] = 376;  
					yygh[167] = 376;  
					yygh[168] = 376;  
					yygh[169] = 376;  
					yygh[170] = 376;  
					yygh[171] = 376;  
					yygh[172] = 376;  
					yygh[173] = 376;  
					yygh[174] = 390;  
					yygh[175] = 390;  
					yygh[176] = 390;  
					yygh[177] = 391;  
					yygh[178] = 392;  
					yygh[179] = 393;  
					yygh[180] = 394;  
					yygh[181] = 394;  
					yygh[182] = 394;  
					yygh[183] = 394;  
					yygh[184] = 394;  
					yygh[185] = 394;  
					yygh[186] = 394;  
					yygh[187] = 395;  
					yygh[188] = 419;  
					yygh[189] = 419;  
					yygh[190] = 419;  
					yygh[191] = 436;  
					yygh[192] = 436;  
					yygh[193] = 436;  
					yygh[194] = 436;  
					yygh[195] = 452;  
					yygh[196] = 458;  
					yygh[197] = 458;  
					yygh[198] = 458;  
					yygh[199] = 458;  
					yygh[200] = 458;  
					yygh[201] = 458;  
					yygh[202] = 458;  
					yygh[203] = 458;  
					yygh[204] = 473;  
					yygh[205] = 473;  
					yygh[206] = 473;  
					yygh[207] = 473;  
					yygh[208] = 473;  
					yygh[209] = 473;  
					yygh[210] = 473;  
					yygh[211] = 473;  
					yygh[212] = 473;  
					yygh[213] = 473;  
					yygh[214] = 473;  
					yygh[215] = 474;  
					yygh[216] = 474;  
					yygh[217] = 474;  
					yygh[218] = 492;  
					yygh[219] = 510;  
					yygh[220] = 527;  
					yygh[221] = 551;  
					yygh[222] = 551;  
					yygh[223] = 565;  
					yygh[224] = 565;  
					yygh[225] = 565;  
					yygh[226] = 565;  
					yygh[227] = 580;  
					yygh[228] = 580;  
					yygh[229] = 580;  
					yygh[230] = 596;  
					yygh[231] = 596;  
					yygh[232] = 596;  
					yygh[233] = 596;  
					yygh[234] = 596;  
					yygh[235] = 613;  
					yygh[236] = 613;  
					yygh[237] = 613;  
					yygh[238] = 631;  
					yygh[239] = 650;  
					yygh[240] = 670;  
					yygh[241] = 691;  
					yygh[242] = 713;  
					yygh[243] = 737;  
					yygh[244] = 737;  
					yygh[245] = 737;  
					yygh[246] = 754;  
					yygh[247] = 771;  
					yygh[248] = 771;  
					yygh[249] = 772;  
					yygh[250] = 772;  
					yygh[251] = 772;  
					yygh[252] = 772;  
					yygh[253] = 772;  
					yygh[254] = 772;  
					yygh[255] = 772;  
					yygh[256] = 772;  
					yygh[257] = 772;  
					yygh[258] = 779;  
					yygh[259] = 786;  
					yygh[260] = 789;  
					yygh[261] = 789;  
					yygh[262] = 789;  
					yygh[263] = 808;  
					yygh[264] = 808;  
					yygh[265] = 808;  
					yygh[266] = 808;  
					yygh[267] = 808;  
					yygh[268] = 808;  
					yygh[269] = 808;  
					yygh[270] = 809;  
					yygh[271] = 810;  
					yygh[272] = 811;  
					yygh[273] = 812;  
					yygh[274] = 812;  
					yygh[275] = 812;  
					yygh[276] = 812;  
					yygh[277] = 812;  
					yygh[278] = 812;  
					yygh[279] = 812;  
					yygh[280] = 812;  
					yygh[281] = 812;  
					yygh[282] = 812;  
					yygh[283] = 828;  
					yygh[284] = 831;  
					yygh[285] = 831;  
					yygh[286] = 831;  
					yygh[287] = 831;  
					yygh[288] = 831;  
					yygh[289] = 831;  
					yygh[290] = 831;  
					yygh[291] = 831;  
					yygh[292] = 831;  
					yygh[293] = 831;  
					yygh[294] = 831;  
					yygh[295] = 831;  
					yygh[296] = 831;  
					yygh[297] = 831;  
					yygh[298] = 831;  
					yygh[299] = 831;  
					yygh[300] = 831;  
					yygh[301] = 831;  
					yygh[302] = 831;  
					yygh[303] = 831;  
					yygh[304] = 848;  
					yygh[305] = 854;  
					yygh[306] = 854;  
					yygh[307] = 854;  
					yygh[308] = 854;  
					yygh[309] = 854; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++;
                }

                public bool yyact(int state, int sym , ref int act)
                {
                        int k = yyal[state];
                        while ( k <= yyah[state] && yya[k].sym != sym) k++;
                        if (k > yyah[state]) return false;
                        act = yya[k].act;
                        return true;
                }
                public bool yygoto(int state, int sym , ref int nstate)
                {
                        int k = yygl[state];
                        while ( k <= yygh[state] && yyg[k].sym != sym) k++;
                        if (k > yygh[state]) return false;
                        nstate = yyg[k].act;
                        return true;
                }

                public void yyerror (string s)
                {
                        System.Console.Write(s);
                }

                int yylexpos = -1;
                string yylval = "";

                public int yylex ()
                {
                        yylexpos++;
                        if(yylexpos >= TokenList.Count)
                        {
                                yylval = "";
                                return 0;
                        }
                        else
                        {
                                yylval = ((AToken)TokenList[yylexpos]).val;
                                return ((AToken)TokenList[yylexpos]).token;
                        }
                }

                public bool yyparse ()
                {

                        parse:

                                yysp++;
                        if (yysp>=yymaxdepth)
                        {
                                yyerror("yyparse stack overflow");
                                goto abort;
                        }

                        yys[yysp] = yystate;
                        yyv[yysp] = yyval;

                        next:

                                if (yyd[yystate]==0 && yychar==-1)
                                {
                                        yychar = yylex();
                                        if (yychar<0) yychar = 0;
                                }

                        yyn = yyd[yystate];
                        if (yyn != 0) goto reduce;


                        if (! yyact(yystate, yychar, ref yyn)) goto error;
                        else if (yyn>0) goto shift;
                        else if (yyn<0) goto reduce;
                        else            goto accept;

                        error:

                                if (yyerrflag==0) yyerror("syntax error");

                        errlab:

                                if (yyerrflag==0) yynerrs++;

                        if (yyerrflag<=2)
                        {
                                yyerrflag = 3;
                                while (yysp>0 && !(yyact(yys[yysp], 255, ref yyn) && yyn > 0)) yysp--;

                                if (yysp==0) goto abort;
                                yystate = yyn;
                                goto parse;
                        }
                        else
                        {
                                if (yychar==0) goto abort;
                                yychar = -1; goto next;
                        }

                        shift:

                        yystate = yyn;
                        yychar = -1;
                        yyval = yylval;
                        if (yyerrflag>0) yyerrflag--;
                        goto parse;

                        reduce:

                        yyflag = yyfnone;
                        yyaction(-yyn);
                        yysp -= yyr[-yyn].len;

                        if (yygoto(yys[yysp], yyr[-yyn].sym, ref yyn)) yystate = yyn;

                        switch (yyflag)
                        {
                                case 1 : goto accept;
                                case 2 : goto abort;
                                case 3 : goto errlab;
                        }

                        goto parse;

                        accept:

                                return true;

                        abort:

                                return false;
                }
		////////////////////////////////////////////////////////////////
		/// Scanner
		////////////////////////////////////////////////////////////////

		public class AToken
		{
			public int token;
			public string val;
		}

		ArrayList TokenList = new ArrayList();

		public bool Scanner (string Input)
		{
		        if (Input.Length == 0) return true;
			TokenList = new ArrayList();
			while (1==1)
			{
				AToken lasttoken = FindToken(Input);
				if (lasttoken.token == 0) break;
				if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
				if (Input.Length > lasttoken.val.Length)
				Input = Input.Substring(lasttoken.val.Length); else return true;
			}
                      System.Console.WriteLine(Input);
			System.Console.WriteLine();
			System.Console.WriteLine("No matching token found!");
			return false;
		}
		public AToken FindToken (string Rest)
		{
			ArrayList Results  = new ArrayList();
			ArrayList ResultsV = new ArrayList();
                      try{

			if (Regex.IsMatch(Rest,"^((?i)IFDEF)")){
				Results.Add (t_IFDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((,[\\s\\t]*)?;+)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^((,[\\s\\t]*)?;+)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IFNDEF)")){
				Results.Add (t_IFNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IFELSE)")){
				Results.Add (t_IFELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ENDIF)")){
				Results.Add (t_ENDIF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ENDIF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DEFINE)")){
				Results.Add (t_DEFINE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DEFINE)").Value);}

			if (Regex.IsMatch(Rest,"^(,)")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^(,)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\}([\\t\\s]*;+)?)")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\}([\\t\\s]*;+)?)").Value);}

			if (Regex.IsMatch(Rest,"^(:)")){
				Results.Add (t_Char58);
				ResultsV.Add(Regex.Match(Rest,"^(:)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|\\|)")){
				Results.Add (t_Char124Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(&&)")){
				Results.Add (t_Char38Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|)")){
				Results.Add (t_Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(\\^)")){
				Results.Add (t_Char94);
				ResultsV.Add(Regex.Match(Rest,"^(\\^)").Value);}

			if (Regex.IsMatch(Rest,"^(&)")){
				Results.Add (t_Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\()")){
				Results.Add (t_Char40);
				ResultsV.Add(Regex.Match(Rest,"^(\\()").Value);}

			if (Regex.IsMatch(Rest,"^(\\))")){
				Results.Add (t_Char41);
				ResultsV.Add(Regex.Match(Rest,"^(\\))").Value);}

			if (Regex.IsMatch(Rest,"^(!=)")){
				Results.Add (t_Char33Char61);
				ResultsV.Add(Regex.Match(Rest,"^(!=)").Value);}

			if (Regex.IsMatch(Rest,"^(==)")){
				Results.Add (t_Char61Char61);
				ResultsV.Add(Regex.Match(Rest,"^(==)").Value);}

			if (Regex.IsMatch(Rest,"^(<)")){
				Results.Add (t_Char60);
				ResultsV.Add(Regex.Match(Rest,"^(<)").Value);}

			if (Regex.IsMatch(Rest,"^(<=)")){
				Results.Add (t_Char60Char61);
				ResultsV.Add(Regex.Match(Rest,"^(<=)").Value);}

			if (Regex.IsMatch(Rest,"^(>)")){
				Results.Add (t_Char62);
				ResultsV.Add(Regex.Match(Rest,"^(>)").Value);}

			if (Regex.IsMatch(Rest,"^(>=)")){
				Results.Add (t_Char62Char61);
				ResultsV.Add(Regex.Match(Rest,"^(>=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+)")){
				Results.Add (t_Char43);
				ResultsV.Add(Regex.Match(Rest,"^(\\+)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-)")){
				Results.Add (t_Char45);
				ResultsV.Add(Regex.Match(Rest,"^(\\-)").Value);}

			if (Regex.IsMatch(Rest,"^(%)")){
				Results.Add (t_Char37);
				ResultsV.Add(Regex.Match(Rest,"^(%)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*)")){
				Results.Add (t_Char42);
				ResultsV.Add(Regex.Match(Rest,"^(\\*)").Value);}

			if (Regex.IsMatch(Rest,"^(/)")){
				Results.Add (t_Char47);
				ResultsV.Add(Regex.Match(Rest,"^(/)").Value);}

			if (Regex.IsMatch(Rest,"^(!)")){
				Results.Add (t_Char33);
				ResultsV.Add(Regex.Match(Rest,"^(!)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)RULE)")){
				Results.Add (t_RULE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RULE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*[\\s\\t]*=)")){
				Results.Add (t_Char42Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\*[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+[\\s\\t]*=)")){
				Results.Add (t_Char43Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\+[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-[\\s\\t]*=)")){
				Results.Add (t_Char45Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\-[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(/[\\s\\t]*=)")){
				Results.Add (t_Char47Char61);
				ResultsV.Add(Regex.Match(Rest,"^(/[\\s\\t]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(=)")){
				Results.Add (t_Char61);
				ResultsV.Add(Regex.Match(Rest,"^(=)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ELSE)")){
				Results.Add (t_ELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IF)")){
				Results.Add (t_IF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)WHILE)")){
				Results.Add (t_WHILE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)WHILE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)NULL)")){
				Results.Add (t_NULL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)NULL)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(IF_MIDDLE|IF_ANYKEY|IF_EQUALS|IF_PERIOD|IF_BRACKL|IF_BRACKR|IF_START|IF_RIGHT|IF_MSTOP|IF_SPACE|IF_PAUSE|IF_COMMA|IF_SEMIC|IF_SLASH|EACH_SEC|MESSAGES|IF_LOAD|IF_LEFT|IF_CTRL|IF_BKSP|IF_PGUP|IF_PGDN|IF_HOME|IF_BKSL|IF_ESC|IF_TAB|IF_ALT|IF_CUU|IF_CUD|IF_CUR|IF_CUL|IF_END|IF_INS|IF_DEL|IF_CAR|IF_CAL|PANELS|LAYERS|(IF_F(1[0-2]|[1-9]))|IF_[0-9A-Z]))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(IF_MIDDLE|IF_ANYKEY|IF_EQUALS|IF_PERIOD|IF_BRACKL|IF_BRACKR|IF_START|IF_RIGHT|IF_MSTOP|IF_SPACE|IF_PAUSE|IF_COMMA|IF_SEMIC|IF_SLASH|EACH_SEC|MESSAGES|IF_LOAD|IF_LEFT|IF_CTRL|IF_BKSP|IF_PGUP|IF_PGDN|IF_HOME|IF_BKSL|IF_ESC|IF_TAB|IF_ALT|IF_CUU|IF_CUD|IF_CUR|IF_CUL|IF_END|IF_INS|IF_DEL|IF_CAR|IF_CAL|PANELS|LAYERS|(IF_F(1[0-2]|[1-9]))|IF_[0-9A-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER))")){
				Results.Add (t_global);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))")){
				Results.Add (t_asset);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))")){
				Results.Add (t_object);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTION|RULES))")){
				Results.Add (t_function);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTION|RULES))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|TAN|SIGN|ABS|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|TAN|SIGN|ABS|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(TEXTURE[1-4]|FLOOR_OFFS_X|FLOOR_OFFS_Y|CEIL_OFFS_X|CEIL_OFFS_Y|FLOOR_ANGLE|IF_RELEASE|SKILL[1-8]|EACH_CYCLE|CEIL_ANGLE|IF_ARRIVED|TARGET_MAP|MAP_COLOR|FLOOR_TEX|REL_ANGLE|OFFSET_X|OFFSET_Y|SCALE_XY|RADIANCE|IF_TOUCH|POSITION|DISTANCE|CEIL_TEX|IF_LEAVE|IF_ARISE|WAYPOINT|TARGET_X|TARGET_Y|REL_DIST|PALFILE|SCALE_X|SCALE_Y|SCYCLES|IF_NEAR|IF_DIVE|PAN_MAP|VSLIDER|HSLIDER|PICTURE|STRINGS|DEFAULT|CYCLES|MIRROR|ALBEDO|SVDIST|ATTACH|LENGTH|SIZE_X|SIZE_Y|IF_FAR|GENIUS|TARGET|HEIGHT|VSPEED|ASPEED|BUTTON|DIGITS|ASPECT|INDEX|RANGE|FLAGS|SIDES|FRAME|TITLE|DELAY|SDIST|POS_X|POS_Y|TOUCH|CYCLE|BELOW|ANGLE|SPEED|BMAPS|OVLYS|LAYER|RIGHT|SVOL|DIST|SIDE|VBAR|HBAR|MASK|LEFT|TYPE|TOP|VAL|MIN|MAX|X1|Y1|Z1|X2|Y2|Z2|X|Y|Z))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(TEXTURE[1-4]|FLOOR_OFFS_X|FLOOR_OFFS_Y|CEIL_OFFS_X|CEIL_OFFS_Y|FLOOR_ANGLE|IF_RELEASE|SKILL[1-8]|EACH_CYCLE|CEIL_ANGLE|IF_ARRIVED|TARGET_MAP|MAP_COLOR|FLOOR_TEX|REL_ANGLE|OFFSET_X|OFFSET_Y|SCALE_XY|RADIANCE|IF_TOUCH|POSITION|DISTANCE|CEIL_TEX|IF_LEAVE|IF_ARISE|WAYPOINT|TARGET_X|TARGET_Y|REL_DIST|PALFILE|SCALE_X|SCALE_Y|SCYCLES|IF_NEAR|IF_DIVE|PAN_MAP|VSLIDER|HSLIDER|PICTURE|STRINGS|DEFAULT|CYCLES|MIRROR|ALBEDO|SVDIST|ATTACH|LENGTH|SIZE_X|SIZE_Y|IF_FAR|GENIUS|TARGET|HEIGHT|VSPEED|ASPEED|BUTTON|DIGITS|ASPECT|INDEX|RANGE|FLAGS|SIDES|FRAME|TITLE|DELAY|SDIST|POS_X|POS_Y|TOUCH|CYCLE|BELOW|ANGLE|SPEED|BMAPS|OVLYS|LAYER|RIGHT|SVOL|DIST|SIDE|VBAR|HBAR|MASK|LEFT|TYPE|TOP|VAL|MIN|MAX|X1|Y1|Z1|X2|Y2|Z2|X|Y|Z))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(PLAY_SOUNDFILE|PLAY_SONG_ONCE|PLAY_FLICFILE|NEXT_MY_THERE|PRINT_STRING|PRINT_VALUE|PLAY_SOUND|STOP_SOUND|SET_STRING|SCREENSHOT|NEXT_THERE|ADD_STRING|RANDOMIZE|PLAY_SONG|PLAY_FLIC|STOP_FLIC|SAVE_INFO|LOAD_INFO|SAVE_DEMO|PLAY_DEMO|STOP_DEMO|EXCLUSIVE|PRINTFILE|SET_SKILL|TO_STRING|IF_NEQUAL|IF_ABOVE|IF_BELOW|IF_EQUAL|FADE_PAL|MIDI_COM|SET_INFO|SET_ALL|PLAY_CD|OUTPORT|SETMIDI|GETMIDI|EXPLODE|NEXT_MY|IF_MIN|IF_MAX|BRANCH|INPORT|FREEZE|LOCATE|ROTATE|ACCEL|WAITT|INKEY|PLACE|SHOOT|SHAKE|SHIFT|LEVEL|ADDT|SUBT|SKIP|GOTO|CALL|WAIT|BEEP|FIND|DROP|PUSH|LIFT|TILT|LOAD|EXIT|SET|ADD|SUB|AND|END|MAP))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(PLAY_SOUNDFILE|PLAY_SONG_ONCE|PLAY_FLICFILE|NEXT_MY_THERE|PRINT_STRING|PRINT_VALUE|PLAY_SOUND|STOP_SOUND|SET_STRING|SCREENSHOT|NEXT_THERE|ADD_STRING|RANDOMIZE|PLAY_SONG|PLAY_FLIC|STOP_FLIC|SAVE_INFO|LOAD_INFO|SAVE_DEMO|PLAY_DEMO|STOP_DEMO|EXCLUSIVE|PRINTFILE|SET_SKILL|TO_STRING|IF_NEQUAL|IF_ABOVE|IF_BELOW|IF_EQUAL|FADE_PAL|MIDI_COM|SET_INFO|SET_ALL|PLAY_CD|OUTPORT|SETMIDI|GETMIDI|EXPLODE|NEXT_MY|IF_MIN|IF_MAX|BRANCH|INPORT|FREEZE|LOCATE|ROTATE|ACCEL|WAITT|INKEY|PLACE|SHOOT|SHAKE|SHIFT|LEVEL|ADDT|SUBT|SKIP|GOTO|CALL|WAIT|BEEP|FIND|DROP|PUSH|LIFT|TILT|LOAD|EXIT|SET|ADD|SUB|AND|END|MAP))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTOR_IMPACT_VX|ACTOR_IMPACT_VY|ACTOR_IMPACT_VZ|ACTOR_FLOOR_HGT|ACTIVE_OBJTICKS|ACTOR_CEIL_HGT|ACTIVE_TARGETS|CHANNEL_[0-7]|PLAYER_LAST_X|PLAYER_LAST_Y|SCREEN_WIDTH|COLOR_PLAYER|COLOR_ACTORS|COLOR_THINGS|COLOR_BORDER|MOUSE_MOVING|PLAYER_LIGHT|PLAYER_WIDTH|PLAYER_CLIMB|SHOOT_SECTOR|PLAYER_ANGLE|PLAYER_SPEED|ACCELERATION|PLAYER_DEPTH|MOUSE_MIDDLE|FORCE_STRAFE|ACTIVE_NEXUS|MOTION_BLUR|RENDER_MODE|MAP_EDGE_X1|MAP_EDGE_X2|MAP_EDGE_Y1|MAP_EDGE_Y2|COLOR_WALLS|PANEL_LAYER|MOUSE_ANGLE|TOUCH_STATE|PLAYER_SIZE|WALK_PERIOD|WAVE_PERIOD|PSOUND_TONE|PLAYER_VROT|PLAYER_TILT|SHOOT_RANGE|HIT_MINDIST|SHOOT_ANGLE|SKIP_FRAMES|ACTOR_CLIMB|ACTOR_WIDTH|THING_WIDTH|IMPACT_VROT|SLOPE_AHEAD|DELTA_ANGLE|MOUSE_RIGHT|FORCE_AHEAD|SHIFT_SENSE|MOUSE_SENSE|MAP_CENTERX|MAP_CENTERY|CDAUDIO_VOL|SCREEN_HGT|SKY_OFFS_X|SKY_OFFS_Y|THING_DIST|ACTOR_DIST|MAP_OFFS_X|MAP_OFFS_Y|TEXT_LAYER|MOUSE_MODE|TOUCH_MODE|MOUSE_CALM|MOUSE_TIME|TOUCH_DIST|JOYSTICK_X|JOYSTICK_Y|LIGHT_DIST|PSOUND_VOL|PLAYER_ARC|PLAYER_SIN|PLAYER_COS|PLAYER_HGT|SLOPE_SIDE|MOVE_ANGLE|FLIC_FRAME|MOUSE_LEFT|KEY_EQUALS|KEY_PERIOD|KEY_BRACKL|KEY_BRACKR|FORCE_TILE|DEBUG_MODE|BLUR_MODE|MOVE_MODE|MAP_SCALE|MAP_LAYER|SOUND_VOL|MUSIC_VOL|DARK_DIST|WALK_TIME|PLAYER_VX|PLAYER_VY|PLAYER_VZ|SHOOT_FAC|IMPACT_VX|IMPACT_VY|IMPACT_VZ|BOUNCE_VX|BOUNCE_VY|TIME_CORR|KEY_SHIFT|KEY_SPACE|KEY_PAUSE|KEY_MINUS|KEY_ENTER|KEY_COMMA|KEY_SEMIC|KEY_SLASH|FORCE_ROT|KEY_SENSE|JOY_SENSE|LOAD_MODE|SCREEN_X|SCREEN_Y|CLIPPING|MAP_MAXX|MAP_MINX|MAP_MAXY|MAP_MINY|MAP_MODE|MICKEY_X|MICKEY_Y|FRICTION|HIT_DIST|PLAYER_X|PLAYER_Y|PLAYER_Z|REMOTE_0|REMOTE_1|TIME_FAC|CD_TRACK|KEY_CTRL|KEY_BKSP|KEY_PGUP|KEY_PGDN|KEY_HOME|KEY_PLUS|KEY_BKSL|FORCE_UP|MAX_DIST|MAP_ROT|MOUSE_X|MOUSE_Y|CHANNEL|INERTIA|SHOOT_X|SHOOT_Y|SLOPE_X|SLOPE_Y|KEY_ANY|KEY_ESC|KEY_TAB|KEY_ALT|KEY_CUU|KEY_CUD|KEY_CUR|KEY_CUL|KEY_END|KEY_INS|KEY_DEL|KEY_CAR|KEY_CAL|ACTIONS|STR_LEN|ASPECT|HIT_X|HIT_Y|TICKS|STEPS|JOY_4|ERROR|WALK|WAVE|NODE|SECS|(KEY_F(1[0-2]|[1-9]))|KEY[A-Z0-9]))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTOR_IMPACT_VX|ACTOR_IMPACT_VY|ACTOR_IMPACT_VZ|ACTOR_FLOOR_HGT|ACTIVE_OBJTICKS|ACTOR_CEIL_HGT|ACTIVE_TARGETS|CHANNEL_[0-7]|PLAYER_LAST_X|PLAYER_LAST_Y|SCREEN_WIDTH|COLOR_PLAYER|COLOR_ACTORS|COLOR_THINGS|COLOR_BORDER|MOUSE_MOVING|PLAYER_LIGHT|PLAYER_WIDTH|PLAYER_CLIMB|SHOOT_SECTOR|PLAYER_ANGLE|PLAYER_SPEED|ACCELERATION|PLAYER_DEPTH|MOUSE_MIDDLE|FORCE_STRAFE|ACTIVE_NEXUS|MOTION_BLUR|RENDER_MODE|MAP_EDGE_X1|MAP_EDGE_X2|MAP_EDGE_Y1|MAP_EDGE_Y2|COLOR_WALLS|PANEL_LAYER|MOUSE_ANGLE|TOUCH_STATE|PLAYER_SIZE|WALK_PERIOD|WAVE_PERIOD|PSOUND_TONE|PLAYER_VROT|PLAYER_TILT|SHOOT_RANGE|HIT_MINDIST|SHOOT_ANGLE|SKIP_FRAMES|ACTOR_CLIMB|ACTOR_WIDTH|THING_WIDTH|IMPACT_VROT|SLOPE_AHEAD|DELTA_ANGLE|MOUSE_RIGHT|FORCE_AHEAD|SHIFT_SENSE|MOUSE_SENSE|MAP_CENTERX|MAP_CENTERY|CDAUDIO_VOL|SCREEN_HGT|SKY_OFFS_X|SKY_OFFS_Y|THING_DIST|ACTOR_DIST|MAP_OFFS_X|MAP_OFFS_Y|TEXT_LAYER|MOUSE_MODE|TOUCH_MODE|MOUSE_CALM|MOUSE_TIME|TOUCH_DIST|JOYSTICK_X|JOYSTICK_Y|LIGHT_DIST|PSOUND_VOL|PLAYER_ARC|PLAYER_SIN|PLAYER_COS|PLAYER_HGT|SLOPE_SIDE|MOVE_ANGLE|FLIC_FRAME|MOUSE_LEFT|KEY_EQUALS|KEY_PERIOD|KEY_BRACKL|KEY_BRACKR|FORCE_TILE|DEBUG_MODE|BLUR_MODE|MOVE_MODE|MAP_SCALE|MAP_LAYER|SOUND_VOL|MUSIC_VOL|DARK_DIST|WALK_TIME|PLAYER_VX|PLAYER_VY|PLAYER_VZ|SHOOT_FAC|IMPACT_VX|IMPACT_VY|IMPACT_VZ|BOUNCE_VX|BOUNCE_VY|TIME_CORR|KEY_SHIFT|KEY_SPACE|KEY_PAUSE|KEY_MINUS|KEY_ENTER|KEY_COMMA|KEY_SEMIC|KEY_SLASH|FORCE_ROT|KEY_SENSE|JOY_SENSE|LOAD_MODE|SCREEN_X|SCREEN_Y|CLIPPING|MAP_MAXX|MAP_MINX|MAP_MAXY|MAP_MINY|MAP_MODE|MICKEY_X|MICKEY_Y|FRICTION|HIT_DIST|PLAYER_X|PLAYER_Y|PLAYER_Z|REMOTE_0|REMOTE_1|TIME_FAC|CD_TRACK|KEY_CTRL|KEY_BKSP|KEY_PGUP|KEY_PGDN|KEY_HOME|KEY_PLUS|KEY_BKSL|FORCE_UP|MAX_DIST|MAP_ROT|MOUSE_X|MOUSE_Y|CHANNEL|INERTIA|SHOOT_X|SHOOT_Y|SLOPE_X|SLOPE_Y|KEY_ANY|KEY_ESC|KEY_TAB|KEY_ALT|KEY_CUU|KEY_CUD|KEY_CUR|KEY_CUL|KEY_END|KEY_INS|KEY_DEL|KEY_CAR|KEY_CAL|ACTIONS|STR_LEN|ASPECT|HIT_X|HIT_Y|TICKS|STEPS|JOY_4|ERROR|WALK|WAVE|NODE|SECS|(KEY_F(1[0-2]|[1-9]))|KEY[A-Z0-9]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)CLIP_DIST)")){
				Results.Add (t_ambigChar95globalChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)CLIP_DIST)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))")){
				Results.Add (t_ambigChar95eventChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THING|ACTOR))")){
				Results.Add (t_ambigChar95objectChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THING|ACTOR))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(SIN|ASIN|SQRT))")){
				Results.Add (t_ambigChar95mathChar95command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(SIN|ASIN|SQRT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)RANDOM)")){
				Results.Add (t_ambigChar95mathChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RANDOM)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)HERE)")){
				Results.Add (t_ambigChar95synonymChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)HERE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT))")){
				Results.Add (t_ambigChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SAVE)")){
				Results.Add (t_ambigChar95commandChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SAVE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)MSPRITE)")){
				Results.Add (t_ambigChar95globalChar95synonymChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MSPRITE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DO)")){
				Results.Add (t_ambigChar95commandChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DO)").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))")){
				Results.Add (t_fixed);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)").Value);}

			if (Regex.IsMatch(Rest,"^(\"(.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(.|[\\r\\n])*?\")").Value);}

			if (Regex.IsMatch(Rest,"^([\\r\\n\\t\\s]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")){
				Results.Add (t_ignore);
				ResultsV.Add(Regex.Match(Rest,"^([\\r\\n\\t\\s]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))").Value);}

			}catch{}
			int maxlength = 0;
			int besttoken = 0;
			AToken ret = new AToken();
		        ret.token = besttoken;
			for (int i = 0; i < Results.Count; i++){
				if (ResultsV[i].ToString().Length > maxlength)
				{
					maxlength = ResultsV[i].ToString().Length;
					besttoken = (int)Results[i];
		         	        ret.token = besttoken;
		                  	if (besttoken != 0)
			                ret.val   = ResultsV[i].ToString();
				}
			}
			return ret;
		}


	}
}
