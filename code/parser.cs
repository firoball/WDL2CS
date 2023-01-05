using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WDL2CS;

namespace VCCCompiler
{
        /// <summary>
        /// Zusammenfassung für MyCompiler.
        /// </summary>
        class MyCompiler
        {
			List<int> tList = new List<int>();
			List<Regex> rList = new List<Regex>();
			MyCompiler()
			{
            tList.Add(t_IFDEF);
            rList.Add(new Regex("^((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("^((,[\\s\\t]*)?;+)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("^((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("^((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("^((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("^((?i)DEFINE)"));
            tList.Add(t_Char44);
            rList.Add(new Regex("^(,)"));
            tList.Add(t_UNDEF);
            rList.Add(new Regex("^((?i)UNDEF)"));
            tList.Add(t_INCLUDE);
            rList.Add(new Regex("^((?i)INCLUDE)"));
            tList.Add(t_Char123);
            rList.Add(new Regex("^(\\{)"));
            tList.Add(t_Char125);
            rList.Add(new Regex("^(\\}([\\t\\s]*;+)?)"));
            tList.Add(t_Char58);
            rList.Add(new Regex("^(:)"));
            tList.Add(t_Char124Char124);
            rList.Add(new Regex("^(\\|\\|)"));
            tList.Add(t_Char38Char38);
            rList.Add(new Regex("^(&&)"));
            tList.Add(t_Char124);
            rList.Add(new Regex("^(\\|)"));
            tList.Add(t_Char94);
            rList.Add(new Regex("^(\\^)"));
            tList.Add(t_Char38);
            rList.Add(new Regex("^(&)"));
            tList.Add(t_Char40);
            rList.Add(new Regex("^(\\()"));
            tList.Add(t_Char41);
            rList.Add(new Regex("^(\\))"));
            tList.Add(t_Char33Char61);
            rList.Add(new Regex("^(!=)"));
            tList.Add(t_Char61Char61);
            rList.Add(new Regex("^(==)"));
            tList.Add(t_Char60);
            rList.Add(new Regex("^(<)"));
            tList.Add(t_Char60Char61);
            rList.Add(new Regex("^(<=)"));
            tList.Add(t_Char62);
            rList.Add(new Regex("^(>)"));
            tList.Add(t_Char62Char61);
            rList.Add(new Regex("^(>=)"));
            tList.Add(t_Char43);
            rList.Add(new Regex("^(\\+)"));
            tList.Add(t_Char45);
            rList.Add(new Regex("^(\\-)"));
            tList.Add(t_Char37);
            rList.Add(new Regex("^(%)"));
            tList.Add(t_Char42);
            rList.Add(new Regex("^(\\*)"));
            tList.Add(t_Char47);
            rList.Add(new Regex("^(/)"));
            tList.Add(t_Char33);
            rList.Add(new Regex("^(!)"));
            tList.Add(t_RULE);
            rList.Add(new Regex("^((?i)RULE)"));
            tList.Add(t_Char42Char61);
            rList.Add(new Regex("^(\\*[\\s\\t]*=)"));
            tList.Add(t_Char43Char61);
            rList.Add(new Regex("^(\\+[\\s\\t]*=)"));
            tList.Add(t_Char45Char61);
            rList.Add(new Regex("^(\\-[\\s\\t]*=)"));
            tList.Add(t_Char47Char61);
            rList.Add(new Regex("^(/[\\s\\t]*=)"));
            tList.Add(t_Char61);
            rList.Add(new Regex("^(=)"));
            tList.Add(t_ELSE);
            rList.Add(new Regex("^((?i)ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("^((?i)IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("^((?i)WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("^(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("^((?i)NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER))"));
            tList.Add(t_asset);
            rList.Add(new Regex("^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("^((?i)(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_flag);
            rList.Add(new Regex("^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("^((?i)(ALBEDO|ANGLE|ASPECT|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRICTION|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS_[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|NODE|PANEL_LAYER|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(CORR|FAC)|TOUCH_(DIST|MODE|STATE)|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("^((?i)CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("^((?i)(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("^((?i)(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("^((?i)RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("^((?i)HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT))"));
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("^((?i)SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("^((?i)MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("^((?i)DO)"));
            tList.Add(t_integer);
            rList.Add(new Regex("^([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("^([A-Za-z0-9][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("^(\"(.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("^([\\r\\n\\t\\s]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
        }
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
                //StreamWriter Output;
                TextWriter Output;

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

//                        if (!compiler.Scanner(inputstream)) return 1;
                        if (!compiler.ScannerOpt(inputstream)) return 1;
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
         //yyval = Globals.BuildGlobals(); + Assets.BuildAssets(); + Objects.BuildObjects();
         Output.WriteLine(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-1] + "\n" + yyv[yysp-0];
         //Console.WriteLine(yyval);
         
       break;
							case    3 : 
         yyval = "";
         
       break;
							case    4 : 
         yyval = yyv[yysp-0];
         //yyval = "";
         
       break;
							case    5 : 
         yyval = yyv[yysp-0];
         //Console.WriteLine(yyval);
         
       break;
							case    6 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Output.WriteLine(yyval);
         
       break;
							case    7 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         //Output.WriteLine(yyval);
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         //Output.WriteLine(yyval);
         
       break;
							case    9 : 
         yyval = yyv[yysp-0];
         //Output.WriteLine(yyval);
         
       break;
							case   10 : 
         yyval = yyv[yysp-0];
         //Output.WriteLine(yyval);
         
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
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-1]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
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
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   24 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = yyv[yysp-0];
         
       break;
							case   27 : 
         yyval = yyv[yysp-0];
         
       break;
							case   28 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   29 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         //remove from grammar?
         
       break;
							case   30 : 
         //yyval = yyv[yysp-0];
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "//Inc " + yyv[yysp - 1] + " START\n" + Include.Process(yyv[yysp-1]) + "\n//inc " + yyv[yysp - 1] + "END";


       break;
							case   34 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   36 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   37 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   38 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-1];
         
       break;
							case   42 : 
         yyval = yyv[yysp-2] + ", " + yyv[yysp-0];
         
       break;
							case   43 : 
         yyval = yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   48 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Formatter.FormatNumber(yyv[yysp-2]) + ", " + yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case   50 : 
         yyval = "";
         
       break;
							case   51 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   52 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4]);
         
         
       break;
							case   53 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   54 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
         
       break;
							case   55 : 
         yyval = yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   57 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   58 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = "";
         
       break;
							case   61 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         Objects.AddProperty(yyv[yysp-2]);
         yyval = "";
         
       break;
							case   62 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   63 : 
         Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
       break;
							case   64 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = Formatter.FormatNull();
         
       break;
							case   69 : 
         yyval = yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   77 : 
         yyval = yyv[yysp-0];
         
       break;
							case   78 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   79 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   80 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   81 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = "";
         
       break;
							case   85 : 
         yyval = yyv[yysp-0];
         
       break;
							case   86 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateInstruction(yyv[yysp-2]);
         
       break;
							case   87 : 
         //yyval = yyv[yysp-0];
         yyval = Actions.CreateInstruction(yyv[yysp-0]);
         
       break;
							case   88 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   89 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
       break;
							case   90 : 
         yyval = yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = "";
         
       break;
							case   97 : 
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  119 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  120 : 
         yyval = yyv[yysp-0];
         
       break;
							case  121 : 
         yyval = yyv[yysp-0];
         
       break;
							case  122 : 
         yyval = " != ";
         
       break;
							case  123 : 
         yyval = " == ";
         
       break;
							case  124 : 
         yyval = " < ";
         
       break;
							case  125 : 
         yyval = " <= ";
         
       break;
							case  126 : 
         yyval = " > ";
         
       break;
							case  127 : 
         yyval = " >= ";
         
       break;
							case  128 : 
         yyval = " + ";
         
       break;
							case  129 : 
         yyval = " - ";
         
       break;
							case  130 : 
         yyval = " % ";
         
       break;
							case  131 : 
         yyval = " * ";
         
       break;
							case  132 : 
         yyval = " / ";
         
       break;
							case  133 : 
         yyval = "!";
         
       break;
							case  134 : 
         yyval = "+";
         
       break;
							case  135 : 
         yyval = "-";
         
       break;
							case  136 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  137 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  138 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  139 : 
         yyval = " *= ";
         
       break;
							case  140 : 
         yyval = " += ";
         
       break;
							case  141 : 
         yyval = " -= ";
         
       break;
							case  142 : 
         yyval = " /= ";
         
       break;
							case  143 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  148 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  149 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  150 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  155 : 
         yyval = yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = Formatter.FormatObjectId(yyv[yysp-0]);
         
       break;
							case  158 : 
         yyval = yyv[yysp-0];
         
       break;
							case  159 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  160 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  161 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  162 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  163 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  164 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  165 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  166 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  167 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  168 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  169 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  173 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  174 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  175 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  180 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = yyv[yysp-0];
         
       break;
							case  183 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = yyv[yysp-0];
         
       break;
							case  187 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = yyv[yysp-0];
         
       break;
							case  190 : 
         yyval = yyv[yysp-0];
         
       break;
							case  191 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  196 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  197 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  198 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  199 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = yyv[yysp-0];
         
       break;
							case  204 : 
         yyval = yyv[yysp-0];
         
       break;
							case  205 : 
         yyval = yyv[yysp-0];
         
       break;
							case  206 : 
         yyval = yyv[yysp-0];
         
       break;
							case  207 : 
         yyval = yyv[yysp-0];
         
       break;
							case  208 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  209 : 
         yyval = Formatter.FormatString(yyv[yysp-0]);
         
       break;
                                default : return;
                        }
                }

                public void InitTables()
                {
                        ////////////////////////////////////////////////////////////////
                        /// Init Table code:
                        ////////////////////////////////////////////////////////////////

					int yynacts   = 2079;
					int yyngotos  = 906;
					int yynstates = 329;
					int yynrules  = 209;
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
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,53);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
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
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(266,-96 );yyac++; 
					yya[yyac] = new YYARec(258,83);yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(266,-96 );yyac++; 
					yya[yyac] = new YYARec(325,-96 );yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(324,-96 );yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(300,-96 );yyac++; 
					yya[yyac] = new YYARec(301,-96 );yyac++; 
					yya[yyac] = new YYARec(304,-96 );yyac++; 
					yya[yyac] = new YYARec(305,-96 );yyac++; 
					yya[yyac] = new YYARec(306,-96 );yyac++; 
					yya[yyac] = new YYARec(307,-96 );yyac++; 
					yya[yyac] = new YYARec(309,-96 );yyac++; 
					yya[yyac] = new YYARec(314,-96 );yyac++; 
					yya[yyac] = new YYARec(315,-96 );yyac++; 
					yya[yyac] = new YYARec(317,-96 );yyac++; 
					yya[yyac] = new YYARec(318,-96 );yyac++; 
					yya[yyac] = new YYARec(320,-96 );yyac++; 
					yya[yyac] = new YYARec(323,-96 );yyac++; 
					yya[yyac] = new YYARec(258,89);yyac++; 
					yya[yyac] = new YYARec(258,90);yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(258,92);yyac++; 
					yya[yyac] = new YYARec(263,93);yyac++; 
					yya[yyac] = new YYARec(258,94);yyac++; 
					yya[yyac] = new YYARec(258,95);yyac++; 
					yya[yyac] = new YYARec(266,96);yyac++; 
					yya[yyac] = new YYARec(266,98);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(258,-41 );yyac++; 
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
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(308,115);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(259,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(267,-60 );yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(282,-96 );yyac++; 
					yya[yyac] = new YYARec(283,-96 );yyac++; 
					yya[yyac] = new YYARec(287,-96 );yyac++; 
					yya[yyac] = new YYARec(321,-96 );yyac++; 
					yya[yyac] = new YYARec(258,157);yyac++; 
					yya[yyac] = new YYARec(260,158);yyac++; 
					yya[yyac] = new YYARec(261,159);yyac++; 
					yya[yyac] = new YYARec(258,160);yyac++; 
					yya[yyac] = new YYARec(297,161);yyac++; 
					yya[yyac] = new YYARec(258,-158 );yyac++; 
					yya[yyac] = new YYARec(263,-158 );yyac++; 
					yya[yyac] = new YYARec(266,-158 );yyac++; 
					yya[yyac] = new YYARec(269,-158 );yyac++; 
					yya[yyac] = new YYARec(270,-158 );yyac++; 
					yya[yyac] = new YYARec(271,-158 );yyac++; 
					yya[yyac] = new YYARec(272,-158 );yyac++; 
					yya[yyac] = new YYARec(273,-158 );yyac++; 
					yya[yyac] = new YYARec(275,-158 );yyac++; 
					yya[yyac] = new YYARec(276,-158 );yyac++; 
					yya[yyac] = new YYARec(277,-158 );yyac++; 
					yya[yyac] = new YYARec(278,-158 );yyac++; 
					yya[yyac] = new YYARec(279,-158 );yyac++; 
					yya[yyac] = new YYARec(280,-158 );yyac++; 
					yya[yyac] = new YYARec(281,-158 );yyac++; 
					yya[yyac] = new YYARec(282,-158 );yyac++; 
					yya[yyac] = new YYARec(283,-158 );yyac++; 
					yya[yyac] = new YYARec(284,-158 );yyac++; 
					yya[yyac] = new YYARec(285,-158 );yyac++; 
					yya[yyac] = new YYARec(286,-158 );yyac++; 
					yya[yyac] = new YYARec(287,-158 );yyac++; 
					yya[yyac] = new YYARec(289,-158 );yyac++; 
					yya[yyac] = new YYARec(290,-158 );yyac++; 
					yya[yyac] = new YYARec(291,-158 );yyac++; 
					yya[yyac] = new YYARec(292,-158 );yyac++; 
					yya[yyac] = new YYARec(293,-158 );yyac++; 
					yya[yyac] = new YYARec(298,-158 );yyac++; 
					yya[yyac] = new YYARec(299,-158 );yyac++; 
					yya[yyac] = new YYARec(300,-158 );yyac++; 
					yya[yyac] = new YYARec(301,-158 );yyac++; 
					yya[yyac] = new YYARec(302,-158 );yyac++; 
					yya[yyac] = new YYARec(304,-158 );yyac++; 
					yya[yyac] = new YYARec(305,-158 );yyac++; 
					yya[yyac] = new YYARec(306,-158 );yyac++; 
					yya[yyac] = new YYARec(307,-158 );yyac++; 
					yya[yyac] = new YYARec(308,-158 );yyac++; 
					yya[yyac] = new YYARec(309,-158 );yyac++; 
					yya[yyac] = new YYARec(310,-158 );yyac++; 
					yya[yyac] = new YYARec(312,-158 );yyac++; 
					yya[yyac] = new YYARec(313,-158 );yyac++; 
					yya[yyac] = new YYARec(314,-158 );yyac++; 
					yya[yyac] = new YYARec(315,-158 );yyac++; 
					yya[yyac] = new YYARec(316,-158 );yyac++; 
					yya[yyac] = new YYARec(317,-158 );yyac++; 
					yya[yyac] = new YYARec(318,-158 );yyac++; 
					yya[yyac] = new YYARec(319,-158 );yyac++; 
					yya[yyac] = new YYARec(320,-158 );yyac++; 
					yya[yyac] = new YYARec(321,-158 );yyac++; 
					yya[yyac] = new YYARec(322,-158 );yyac++; 
					yya[yyac] = new YYARec(323,-158 );yyac++; 
					yya[yyac] = new YYARec(324,-158 );yyac++; 
					yya[yyac] = new YYARec(325,-158 );yyac++; 
					yya[yyac] = new YYARec(297,162);yyac++; 
					yya[yyac] = new YYARec(258,-157 );yyac++; 
					yya[yyac] = new YYARec(263,-157 );yyac++; 
					yya[yyac] = new YYARec(266,-157 );yyac++; 
					yya[yyac] = new YYARec(269,-157 );yyac++; 
					yya[yyac] = new YYARec(270,-157 );yyac++; 
					yya[yyac] = new YYARec(271,-157 );yyac++; 
					yya[yyac] = new YYARec(272,-157 );yyac++; 
					yya[yyac] = new YYARec(273,-157 );yyac++; 
					yya[yyac] = new YYARec(275,-157 );yyac++; 
					yya[yyac] = new YYARec(276,-157 );yyac++; 
					yya[yyac] = new YYARec(277,-157 );yyac++; 
					yya[yyac] = new YYARec(278,-157 );yyac++; 
					yya[yyac] = new YYARec(279,-157 );yyac++; 
					yya[yyac] = new YYARec(280,-157 );yyac++; 
					yya[yyac] = new YYARec(281,-157 );yyac++; 
					yya[yyac] = new YYARec(282,-157 );yyac++; 
					yya[yyac] = new YYARec(283,-157 );yyac++; 
					yya[yyac] = new YYARec(284,-157 );yyac++; 
					yya[yyac] = new YYARec(285,-157 );yyac++; 
					yya[yyac] = new YYARec(286,-157 );yyac++; 
					yya[yyac] = new YYARec(287,-157 );yyac++; 
					yya[yyac] = new YYARec(289,-157 );yyac++; 
					yya[yyac] = new YYARec(290,-157 );yyac++; 
					yya[yyac] = new YYARec(291,-157 );yyac++; 
					yya[yyac] = new YYARec(292,-157 );yyac++; 
					yya[yyac] = new YYARec(293,-157 );yyac++; 
					yya[yyac] = new YYARec(298,-157 );yyac++; 
					yya[yyac] = new YYARec(299,-157 );yyac++; 
					yya[yyac] = new YYARec(300,-157 );yyac++; 
					yya[yyac] = new YYARec(301,-157 );yyac++; 
					yya[yyac] = new YYARec(302,-157 );yyac++; 
					yya[yyac] = new YYARec(304,-157 );yyac++; 
					yya[yyac] = new YYARec(305,-157 );yyac++; 
					yya[yyac] = new YYARec(306,-157 );yyac++; 
					yya[yyac] = new YYARec(307,-157 );yyac++; 
					yya[yyac] = new YYARec(308,-157 );yyac++; 
					yya[yyac] = new YYARec(309,-157 );yyac++; 
					yya[yyac] = new YYARec(310,-157 );yyac++; 
					yya[yyac] = new YYARec(312,-157 );yyac++; 
					yya[yyac] = new YYARec(313,-157 );yyac++; 
					yya[yyac] = new YYARec(314,-157 );yyac++; 
					yya[yyac] = new YYARec(315,-157 );yyac++; 
					yya[yyac] = new YYARec(316,-157 );yyac++; 
					yya[yyac] = new YYARec(317,-157 );yyac++; 
					yya[yyac] = new YYARec(318,-157 );yyac++; 
					yya[yyac] = new YYARec(319,-157 );yyac++; 
					yya[yyac] = new YYARec(320,-157 );yyac++; 
					yya[yyac] = new YYARec(321,-157 );yyac++; 
					yya[yyac] = new YYARec(322,-157 );yyac++; 
					yya[yyac] = new YYARec(323,-157 );yyac++; 
					yya[yyac] = new YYARec(324,-157 );yyac++; 
					yya[yyac] = new YYARec(325,-157 );yyac++; 
					yya[yyac] = new YYARec(258,163);yyac++; 
					yya[yyac] = new YYARec(258,-175 );yyac++; 
					yya[yyac] = new YYARec(263,-175 );yyac++; 
					yya[yyac] = new YYARec(282,-175 );yyac++; 
					yya[yyac] = new YYARec(283,-175 );yyac++; 
					yya[yyac] = new YYARec(287,-175 );yyac++; 
					yya[yyac] = new YYARec(298,-175 );yyac++; 
					yya[yyac] = new YYARec(299,-175 );yyac++; 
					yya[yyac] = new YYARec(300,-175 );yyac++; 
					yya[yyac] = new YYARec(301,-175 );yyac++; 
					yya[yyac] = new YYARec(302,-175 );yyac++; 
					yya[yyac] = new YYARec(304,-175 );yyac++; 
					yya[yyac] = new YYARec(305,-175 );yyac++; 
					yya[yyac] = new YYARec(306,-175 );yyac++; 
					yya[yyac] = new YYARec(307,-175 );yyac++; 
					yya[yyac] = new YYARec(308,-175 );yyac++; 
					yya[yyac] = new YYARec(309,-175 );yyac++; 
					yya[yyac] = new YYARec(310,-175 );yyac++; 
					yya[yyac] = new YYARec(312,-175 );yyac++; 
					yya[yyac] = new YYARec(313,-175 );yyac++; 
					yya[yyac] = new YYARec(314,-175 );yyac++; 
					yya[yyac] = new YYARec(315,-175 );yyac++; 
					yya[yyac] = new YYARec(316,-175 );yyac++; 
					yya[yyac] = new YYARec(317,-175 );yyac++; 
					yya[yyac] = new YYARec(318,-175 );yyac++; 
					yya[yyac] = new YYARec(319,-175 );yyac++; 
					yya[yyac] = new YYARec(320,-175 );yyac++; 
					yya[yyac] = new YYARec(321,-175 );yyac++; 
					yya[yyac] = new YYARec(322,-175 );yyac++; 
					yya[yyac] = new YYARec(323,-175 );yyac++; 
					yya[yyac] = new YYARec(324,-175 );yyac++; 
					yya[yyac] = new YYARec(325,-175 );yyac++; 
					yya[yyac] = new YYARec(268,-200 );yyac++; 
					yya[yyac] = new YYARec(289,-200 );yyac++; 
					yya[yyac] = new YYARec(290,-200 );yyac++; 
					yya[yyac] = new YYARec(291,-200 );yyac++; 
					yya[yyac] = new YYARec(292,-200 );yyac++; 
					yya[yyac] = new YYARec(293,-200 );yyac++; 
					yya[yyac] = new YYARec(297,-200 );yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(258,-87 );yyac++; 
					yya[yyac] = new YYARec(282,-96 );yyac++; 
					yya[yyac] = new YYARec(283,-96 );yyac++; 
					yya[yyac] = new YYARec(287,-96 );yyac++; 
					yya[yyac] = new YYARec(298,-96 );yyac++; 
					yya[yyac] = new YYARec(299,-96 );yyac++; 
					yya[yyac] = new YYARec(300,-96 );yyac++; 
					yya[yyac] = new YYARec(301,-96 );yyac++; 
					yya[yyac] = new YYARec(302,-96 );yyac++; 
					yya[yyac] = new YYARec(304,-96 );yyac++; 
					yya[yyac] = new YYARec(305,-96 );yyac++; 
					yya[yyac] = new YYARec(306,-96 );yyac++; 
					yya[yyac] = new YYARec(307,-96 );yyac++; 
					yya[yyac] = new YYARec(308,-96 );yyac++; 
					yya[yyac] = new YYARec(309,-96 );yyac++; 
					yya[yyac] = new YYARec(310,-96 );yyac++; 
					yya[yyac] = new YYARec(312,-96 );yyac++; 
					yya[yyac] = new YYARec(313,-96 );yyac++; 
					yya[yyac] = new YYARec(314,-96 );yyac++; 
					yya[yyac] = new YYARec(315,-96 );yyac++; 
					yya[yyac] = new YYARec(316,-96 );yyac++; 
					yya[yyac] = new YYARec(317,-96 );yyac++; 
					yya[yyac] = new YYARec(318,-96 );yyac++; 
					yya[yyac] = new YYARec(319,-96 );yyac++; 
					yya[yyac] = new YYARec(320,-96 );yyac++; 
					yya[yyac] = new YYARec(321,-96 );yyac++; 
					yya[yyac] = new YYARec(322,-96 );yyac++; 
					yya[yyac] = new YYARec(323,-96 );yyac++; 
					yya[yyac] = new YYARec(324,-96 );yyac++; 
					yya[yyac] = new YYARec(325,-96 );yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(258,166);yyac++; 
					yya[yyac] = new YYARec(268,167);yyac++; 
					yya[yyac] = new YYARec(297,162);yyac++; 
					yya[yyac] = new YYARec(289,-157 );yyac++; 
					yya[yyac] = new YYARec(290,-157 );yyac++; 
					yya[yyac] = new YYARec(291,-157 );yyac++; 
					yya[yyac] = new YYARec(292,-157 );yyac++; 
					yya[yyac] = new YYARec(293,-157 );yyac++; 
					yya[yyac] = new YYARec(268,-207 );yyac++; 
					yya[yyac] = new YYARec(289,169);yyac++; 
					yya[yyac] = new YYARec(290,170);yyac++; 
					yya[yyac] = new YYARec(291,171);yyac++; 
					yya[yyac] = new YYARec(292,172);yyac++; 
					yya[yyac] = new YYARec(293,173);yyac++; 
					yya[yyac] = new YYARec(267,174);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(266,198);yyac++; 
					yya[yyac] = new YYARec(258,-173 );yyac++; 
					yya[yyac] = new YYARec(263,-173 );yyac++; 
					yya[yyac] = new YYARec(282,-173 );yyac++; 
					yya[yyac] = new YYARec(283,-173 );yyac++; 
					yya[yyac] = new YYARec(287,-173 );yyac++; 
					yya[yyac] = new YYARec(298,-173 );yyac++; 
					yya[yyac] = new YYARec(299,-173 );yyac++; 
					yya[yyac] = new YYARec(300,-173 );yyac++; 
					yya[yyac] = new YYARec(301,-173 );yyac++; 
					yya[yyac] = new YYARec(302,-173 );yyac++; 
					yya[yyac] = new YYARec(304,-173 );yyac++; 
					yya[yyac] = new YYARec(305,-173 );yyac++; 
					yya[yyac] = new YYARec(306,-173 );yyac++; 
					yya[yyac] = new YYARec(307,-173 );yyac++; 
					yya[yyac] = new YYARec(308,-173 );yyac++; 
					yya[yyac] = new YYARec(309,-173 );yyac++; 
					yya[yyac] = new YYARec(310,-173 );yyac++; 
					yya[yyac] = new YYARec(312,-173 );yyac++; 
					yya[yyac] = new YYARec(313,-173 );yyac++; 
					yya[yyac] = new YYARec(314,-173 );yyac++; 
					yya[yyac] = new YYARec(315,-173 );yyac++; 
					yya[yyac] = new YYARec(316,-173 );yyac++; 
					yya[yyac] = new YYARec(317,-173 );yyac++; 
					yya[yyac] = new YYARec(318,-173 );yyac++; 
					yya[yyac] = new YYARec(319,-173 );yyac++; 
					yya[yyac] = new YYARec(320,-173 );yyac++; 
					yya[yyac] = new YYARec(321,-173 );yyac++; 
					yya[yyac] = new YYARec(322,-173 );yyac++; 
					yya[yyac] = new YYARec(323,-173 );yyac++; 
					yya[yyac] = new YYARec(324,-173 );yyac++; 
					yya[yyac] = new YYARec(325,-173 );yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(289,-195 );yyac++; 
					yya[yyac] = new YYARec(290,-195 );yyac++; 
					yya[yyac] = new YYARec(291,-195 );yyac++; 
					yya[yyac] = new YYARec(292,-195 );yyac++; 
					yya[yyac] = new YYARec(293,-195 );yyac++; 
					yya[yyac] = new YYARec(297,-195 );yyac++; 
					yya[yyac] = new YYARec(268,-206 );yyac++; 
					yya[yyac] = new YYARec(268,-144 );yyac++; 
					yya[yyac] = new YYARec(289,-144 );yyac++; 
					yya[yyac] = new YYARec(290,-144 );yyac++; 
					yya[yyac] = new YYARec(291,-144 );yyac++; 
					yya[yyac] = new YYARec(292,-144 );yyac++; 
					yya[yyac] = new YYARec(293,-144 );yyac++; 
					yya[yyac] = new YYARec(297,-144 );yyac++; 
					yya[yyac] = new YYARec(258,-174 );yyac++; 
					yya[yyac] = new YYARec(263,-174 );yyac++; 
					yya[yyac] = new YYARec(282,-174 );yyac++; 
					yya[yyac] = new YYARec(283,-174 );yyac++; 
					yya[yyac] = new YYARec(287,-174 );yyac++; 
					yya[yyac] = new YYARec(298,-174 );yyac++; 
					yya[yyac] = new YYARec(299,-174 );yyac++; 
					yya[yyac] = new YYARec(300,-174 );yyac++; 
					yya[yyac] = new YYARec(301,-174 );yyac++; 
					yya[yyac] = new YYARec(302,-174 );yyac++; 
					yya[yyac] = new YYARec(304,-174 );yyac++; 
					yya[yyac] = new YYARec(305,-174 );yyac++; 
					yya[yyac] = new YYARec(306,-174 );yyac++; 
					yya[yyac] = new YYARec(307,-174 );yyac++; 
					yya[yyac] = new YYARec(308,-174 );yyac++; 
					yya[yyac] = new YYARec(309,-174 );yyac++; 
					yya[yyac] = new YYARec(310,-174 );yyac++; 
					yya[yyac] = new YYARec(312,-174 );yyac++; 
					yya[yyac] = new YYARec(313,-174 );yyac++; 
					yya[yyac] = new YYARec(314,-174 );yyac++; 
					yya[yyac] = new YYARec(315,-174 );yyac++; 
					yya[yyac] = new YYARec(316,-174 );yyac++; 
					yya[yyac] = new YYARec(317,-174 );yyac++; 
					yya[yyac] = new YYARec(318,-174 );yyac++; 
					yya[yyac] = new YYARec(319,-174 );yyac++; 
					yya[yyac] = new YYARec(320,-174 );yyac++; 
					yya[yyac] = new YYARec(321,-174 );yyac++; 
					yya[yyac] = new YYARec(322,-174 );yyac++; 
					yya[yyac] = new YYARec(323,-174 );yyac++; 
					yya[yyac] = new YYARec(324,-174 );yyac++; 
					yya[yyac] = new YYARec(325,-174 );yyac++; 
					yya[yyac] = new YYARec(258,202);yyac++; 
					yya[yyac] = new YYARec(268,-198 );yyac++; 
					yya[yyac] = new YYARec(289,-198 );yyac++; 
					yya[yyac] = new YYARec(290,-198 );yyac++; 
					yya[yyac] = new YYARec(291,-198 );yyac++; 
					yya[yyac] = new YYARec(292,-198 );yyac++; 
					yya[yyac] = new YYARec(293,-198 );yyac++; 
					yya[yyac] = new YYARec(297,-198 );yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(282,-96 );yyac++; 
					yya[yyac] = new YYARec(283,-96 );yyac++; 
					yya[yyac] = new YYARec(287,-96 );yyac++; 
					yya[yyac] = new YYARec(298,-96 );yyac++; 
					yya[yyac] = new YYARec(300,-96 );yyac++; 
					yya[yyac] = new YYARec(301,-96 );yyac++; 
					yya[yyac] = new YYARec(302,-96 );yyac++; 
					yya[yyac] = new YYARec(303,-96 );yyac++; 
					yya[yyac] = new YYARec(304,-96 );yyac++; 
					yya[yyac] = new YYARec(305,-96 );yyac++; 
					yya[yyac] = new YYARec(306,-96 );yyac++; 
					yya[yyac] = new YYARec(307,-96 );yyac++; 
					yya[yyac] = new YYARec(309,-96 );yyac++; 
					yya[yyac] = new YYARec(310,-96 );yyac++; 
					yya[yyac] = new YYARec(313,-96 );yyac++; 
					yya[yyac] = new YYARec(314,-96 );yyac++; 
					yya[yyac] = new YYARec(315,-96 );yyac++; 
					yya[yyac] = new YYARec(316,-96 );yyac++; 
					yya[yyac] = new YYARec(317,-96 );yyac++; 
					yya[yyac] = new YYARec(318,-96 );yyac++; 
					yya[yyac] = new YYARec(319,-96 );yyac++; 
					yya[yyac] = new YYARec(320,-96 );yyac++; 
					yya[yyac] = new YYARec(321,-96 );yyac++; 
					yya[yyac] = new YYARec(322,-96 );yyac++; 
					yya[yyac] = new YYARec(323,-96 );yyac++; 
					yya[yyac] = new YYARec(324,-96 );yyac++; 
					yya[yyac] = new YYARec(325,-96 );yyac++; 
					yya[yyac] = new YYARec(258,204);yyac++; 
					yya[yyac] = new YYARec(267,205);yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(259,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(260,-60 );yyac++; 
					yya[yyac] = new YYARec(261,-60 );yyac++; 
					yya[yyac] = new YYARec(267,-60 );yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(258,212);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,215);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(313,216);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(316,217);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(318,218);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,215);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(313,216);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(316,217);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(318,218);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(308,115);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(258,231);yyac++; 
					yya[yyac] = new YYARec(258,232);yyac++; 
					yya[yyac] = new YYARec(267,233);yyac++; 
					yya[yyac] = new YYARec(274,234);yyac++; 
					yya[yyac] = new YYARec(258,-202 );yyac++; 
					yya[yyac] = new YYARec(266,-202 );yyac++; 
					yya[yyac] = new YYARec(269,-202 );yyac++; 
					yya[yyac] = new YYARec(270,-202 );yyac++; 
					yya[yyac] = new YYARec(271,-202 );yyac++; 
					yya[yyac] = new YYARec(272,-202 );yyac++; 
					yya[yyac] = new YYARec(273,-202 );yyac++; 
					yya[yyac] = new YYARec(275,-202 );yyac++; 
					yya[yyac] = new YYARec(276,-202 );yyac++; 
					yya[yyac] = new YYARec(277,-202 );yyac++; 
					yya[yyac] = new YYARec(278,-202 );yyac++; 
					yya[yyac] = new YYARec(279,-202 );yyac++; 
					yya[yyac] = new YYARec(280,-202 );yyac++; 
					yya[yyac] = new YYARec(281,-202 );yyac++; 
					yya[yyac] = new YYARec(282,-202 );yyac++; 
					yya[yyac] = new YYARec(283,-202 );yyac++; 
					yya[yyac] = new YYARec(284,-202 );yyac++; 
					yya[yyac] = new YYARec(285,-202 );yyac++; 
					yya[yyac] = new YYARec(286,-202 );yyac++; 
					yya[yyac] = new YYARec(289,-202 );yyac++; 
					yya[yyac] = new YYARec(290,-202 );yyac++; 
					yya[yyac] = new YYARec(291,-202 );yyac++; 
					yya[yyac] = new YYARec(292,-202 );yyac++; 
					yya[yyac] = new YYARec(293,-202 );yyac++; 
					yya[yyac] = new YYARec(297,-202 );yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(284,237);yyac++; 
					yya[yyac] = new YYARec(285,238);yyac++; 
					yya[yyac] = new YYARec(286,239);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(266,-112 );yyac++; 
					yya[yyac] = new YYARec(269,-112 );yyac++; 
					yya[yyac] = new YYARec(270,-112 );yyac++; 
					yya[yyac] = new YYARec(271,-112 );yyac++; 
					yya[yyac] = new YYARec(272,-112 );yyac++; 
					yya[yyac] = new YYARec(273,-112 );yyac++; 
					yya[yyac] = new YYARec(275,-112 );yyac++; 
					yya[yyac] = new YYARec(276,-112 );yyac++; 
					yya[yyac] = new YYARec(277,-112 );yyac++; 
					yya[yyac] = new YYARec(278,-112 );yyac++; 
					yya[yyac] = new YYARec(279,-112 );yyac++; 
					yya[yyac] = new YYARec(280,-112 );yyac++; 
					yya[yyac] = new YYARec(281,-112 );yyac++; 
					yya[yyac] = new YYARec(282,-112 );yyac++; 
					yya[yyac] = new YYARec(283,-112 );yyac++; 
					yya[yyac] = new YYARec(282,241);yyac++; 
					yya[yyac] = new YYARec(283,242);yyac++; 
					yya[yyac] = new YYARec(258,-110 );yyac++; 
					yya[yyac] = new YYARec(266,-110 );yyac++; 
					yya[yyac] = new YYARec(269,-110 );yyac++; 
					yya[yyac] = new YYARec(270,-110 );yyac++; 
					yya[yyac] = new YYARec(271,-110 );yyac++; 
					yya[yyac] = new YYARec(272,-110 );yyac++; 
					yya[yyac] = new YYARec(273,-110 );yyac++; 
					yya[yyac] = new YYARec(275,-110 );yyac++; 
					yya[yyac] = new YYARec(276,-110 );yyac++; 
					yya[yyac] = new YYARec(277,-110 );yyac++; 
					yya[yyac] = new YYARec(278,-110 );yyac++; 
					yya[yyac] = new YYARec(279,-110 );yyac++; 
					yya[yyac] = new YYARec(280,-110 );yyac++; 
					yya[yyac] = new YYARec(281,-110 );yyac++; 
					yya[yyac] = new YYARec(278,244);yyac++; 
					yya[yyac] = new YYARec(279,245);yyac++; 
					yya[yyac] = new YYARec(280,246);yyac++; 
					yya[yyac] = new YYARec(281,247);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(266,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(273,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(276,-108 );yyac++; 
					yya[yyac] = new YYARec(277,-108 );yyac++; 
					yya[yyac] = new YYARec(276,249);yyac++; 
					yya[yyac] = new YYARec(277,250);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(273,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(273,251);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(272,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(272,252);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(271,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(271,253);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(270,254);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(269,255);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(275,-97 );yyac++; 
					yya[yyac] = new YYARec(289,169);yyac++; 
					yya[yyac] = new YYARec(290,170);yyac++; 
					yya[yyac] = new YYARec(291,171);yyac++; 
					yya[yyac] = new YYARec(292,172);yyac++; 
					yya[yyac] = new YYARec(293,173);yyac++; 
					yya[yyac] = new YYARec(258,-121 );yyac++; 
					yya[yyac] = new YYARec(269,-121 );yyac++; 
					yya[yyac] = new YYARec(270,-121 );yyac++; 
					yya[yyac] = new YYARec(271,-121 );yyac++; 
					yya[yyac] = new YYARec(272,-121 );yyac++; 
					yya[yyac] = new YYARec(273,-121 );yyac++; 
					yya[yyac] = new YYARec(276,-121 );yyac++; 
					yya[yyac] = new YYARec(277,-121 );yyac++; 
					yya[yyac] = new YYARec(278,-121 );yyac++; 
					yya[yyac] = new YYARec(279,-121 );yyac++; 
					yya[yyac] = new YYARec(280,-121 );yyac++; 
					yya[yyac] = new YYARec(281,-121 );yyac++; 
					yya[yyac] = new YYARec(282,-121 );yyac++; 
					yya[yyac] = new YYARec(283,-121 );yyac++; 
					yya[yyac] = new YYARec(284,-121 );yyac++; 
					yya[yyac] = new YYARec(285,-121 );yyac++; 
					yya[yyac] = new YYARec(286,-121 );yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(266,259);yyac++; 
					yya[yyac] = new YYARec(266,260);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,271);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(259,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(260,-60 );yyac++; 
					yya[yyac] = new YYARec(261,-60 );yyac++; 
					yya[yyac] = new YYARec(267,-60 );yyac++; 
					yya[yyac] = new YYARec(258,273);yyac++; 
					yya[yyac] = new YYARec(258,274);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(263,275);yyac++; 
					yya[yyac] = new YYARec(258,-49 );yyac++; 
					yya[yyac] = new YYARec(258,276);yyac++; 
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
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(282,-96 );yyac++; 
					yya[yyac] = new YYARec(283,-96 );yyac++; 
					yya[yyac] = new YYARec(287,-96 );yyac++; 
					yya[yyac] = new YYARec(298,-96 );yyac++; 
					yya[yyac] = new YYARec(299,-96 );yyac++; 
					yya[yyac] = new YYARec(300,-96 );yyac++; 
					yya[yyac] = new YYARec(301,-96 );yyac++; 
					yya[yyac] = new YYARec(302,-96 );yyac++; 
					yya[yyac] = new YYARec(304,-96 );yyac++; 
					yya[yyac] = new YYARec(305,-96 );yyac++; 
					yya[yyac] = new YYARec(306,-96 );yyac++; 
					yya[yyac] = new YYARec(307,-96 );yyac++; 
					yya[yyac] = new YYARec(308,-96 );yyac++; 
					yya[yyac] = new YYARec(309,-96 );yyac++; 
					yya[yyac] = new YYARec(310,-96 );yyac++; 
					yya[yyac] = new YYARec(312,-96 );yyac++; 
					yya[yyac] = new YYARec(313,-96 );yyac++; 
					yya[yyac] = new YYARec(314,-96 );yyac++; 
					yya[yyac] = new YYARec(315,-96 );yyac++; 
					yya[yyac] = new YYARec(316,-96 );yyac++; 
					yya[yyac] = new YYARec(317,-96 );yyac++; 
					yya[yyac] = new YYARec(318,-96 );yyac++; 
					yya[yyac] = new YYARec(319,-96 );yyac++; 
					yya[yyac] = new YYARec(320,-96 );yyac++; 
					yya[yyac] = new YYARec(321,-96 );yyac++; 
					yya[yyac] = new YYARec(322,-96 );yyac++; 
					yya[yyac] = new YYARec(323,-96 );yyac++; 
					yya[yyac] = new YYARec(324,-96 );yyac++; 
					yya[yyac] = new YYARec(325,-96 );yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(260,-84 );yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(274,195);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,196);yyac++; 
					yya[yyac] = new YYARec(322,197);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(275,294);yyac++; 
					yya[yyac] = new YYARec(267,295);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(267,-84 );yyac++; 
					yya[yyac] = new YYARec(297,298);yyac++; 
					yya[yyac] = new YYARec(258,-74 );yyac++; 
					yya[yyac] = new YYARec(263,-74 );yyac++; 
					yya[yyac] = new YYARec(282,-74 );yyac++; 
					yya[yyac] = new YYARec(283,-74 );yyac++; 
					yya[yyac] = new YYARec(287,-74 );yyac++; 
					yya[yyac] = new YYARec(298,-74 );yyac++; 
					yya[yyac] = new YYARec(300,-74 );yyac++; 
					yya[yyac] = new YYARec(301,-74 );yyac++; 
					yya[yyac] = new YYARec(302,-74 );yyac++; 
					yya[yyac] = new YYARec(303,-74 );yyac++; 
					yya[yyac] = new YYARec(304,-74 );yyac++; 
					yya[yyac] = new YYARec(305,-74 );yyac++; 
					yya[yyac] = new YYARec(306,-74 );yyac++; 
					yya[yyac] = new YYARec(307,-74 );yyac++; 
					yya[yyac] = new YYARec(309,-74 );yyac++; 
					yya[yyac] = new YYARec(310,-74 );yyac++; 
					yya[yyac] = new YYARec(313,-74 );yyac++; 
					yya[yyac] = new YYARec(314,-74 );yyac++; 
					yya[yyac] = new YYARec(315,-74 );yyac++; 
					yya[yyac] = new YYARec(316,-74 );yyac++; 
					yya[yyac] = new YYARec(317,-74 );yyac++; 
					yya[yyac] = new YYARec(318,-74 );yyac++; 
					yya[yyac] = new YYARec(319,-74 );yyac++; 
					yya[yyac] = new YYARec(320,-74 );yyac++; 
					yya[yyac] = new YYARec(321,-74 );yyac++; 
					yya[yyac] = new YYARec(322,-74 );yyac++; 
					yya[yyac] = new YYARec(323,-74 );yyac++; 
					yya[yyac] = new YYARec(324,-74 );yyac++; 
					yya[yyac] = new YYARec(325,-74 );yyac++; 
					yya[yyac] = new YYARec(263,81);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(282,-96 );yyac++; 
					yya[yyac] = new YYARec(283,-96 );yyac++; 
					yya[yyac] = new YYARec(287,-96 );yyac++; 
					yya[yyac] = new YYARec(298,-96 );yyac++; 
					yya[yyac] = new YYARec(300,-96 );yyac++; 
					yya[yyac] = new YYARec(301,-96 );yyac++; 
					yya[yyac] = new YYARec(302,-96 );yyac++; 
					yya[yyac] = new YYARec(303,-96 );yyac++; 
					yya[yyac] = new YYARec(304,-96 );yyac++; 
					yya[yyac] = new YYARec(305,-96 );yyac++; 
					yya[yyac] = new YYARec(306,-96 );yyac++; 
					yya[yyac] = new YYARec(307,-96 );yyac++; 
					yya[yyac] = new YYARec(309,-96 );yyac++; 
					yya[yyac] = new YYARec(310,-96 );yyac++; 
					yya[yyac] = new YYARec(313,-96 );yyac++; 
					yya[yyac] = new YYARec(314,-96 );yyac++; 
					yya[yyac] = new YYARec(315,-96 );yyac++; 
					yya[yyac] = new YYARec(316,-96 );yyac++; 
					yya[yyac] = new YYARec(317,-96 );yyac++; 
					yya[yyac] = new YYARec(318,-96 );yyac++; 
					yya[yyac] = new YYARec(319,-96 );yyac++; 
					yya[yyac] = new YYARec(320,-96 );yyac++; 
					yya[yyac] = new YYARec(321,-96 );yyac++; 
					yya[yyac] = new YYARec(322,-96 );yyac++; 
					yya[yyac] = new YYARec(323,-96 );yyac++; 
					yya[yyac] = new YYARec(324,-96 );yyac++; 
					yya[yyac] = new YYARec(325,-96 );yyac++; 
					yya[yyac] = new YYARec(297,300);yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(263,-75 );yyac++; 
					yya[yyac] = new YYARec(282,-75 );yyac++; 
					yya[yyac] = new YYARec(283,-75 );yyac++; 
					yya[yyac] = new YYARec(287,-75 );yyac++; 
					yya[yyac] = new YYARec(298,-75 );yyac++; 
					yya[yyac] = new YYARec(300,-75 );yyac++; 
					yya[yyac] = new YYARec(301,-75 );yyac++; 
					yya[yyac] = new YYARec(302,-75 );yyac++; 
					yya[yyac] = new YYARec(303,-75 );yyac++; 
					yya[yyac] = new YYARec(304,-75 );yyac++; 
					yya[yyac] = new YYARec(305,-75 );yyac++; 
					yya[yyac] = new YYARec(306,-75 );yyac++; 
					yya[yyac] = new YYARec(307,-75 );yyac++; 
					yya[yyac] = new YYARec(309,-75 );yyac++; 
					yya[yyac] = new YYARec(310,-75 );yyac++; 
					yya[yyac] = new YYARec(313,-75 );yyac++; 
					yya[yyac] = new YYARec(314,-75 );yyac++; 
					yya[yyac] = new YYARec(315,-75 );yyac++; 
					yya[yyac] = new YYARec(316,-75 );yyac++; 
					yya[yyac] = new YYARec(317,-75 );yyac++; 
					yya[yyac] = new YYARec(318,-75 );yyac++; 
					yya[yyac] = new YYARec(319,-75 );yyac++; 
					yya[yyac] = new YYARec(320,-75 );yyac++; 
					yya[yyac] = new YYARec(321,-75 );yyac++; 
					yya[yyac] = new YYARec(322,-75 );yyac++; 
					yya[yyac] = new YYARec(323,-75 );yyac++; 
					yya[yyac] = new YYARec(324,-75 );yyac++; 
					yya[yyac] = new YYARec(325,-75 );yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(259,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(260,-60 );yyac++; 
					yya[yyac] = new YYARec(261,-60 );yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(259,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(260,-60 );yyac++; 
					yya[yyac] = new YYARec(261,-60 );yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(258,-50 );yyac++; 
					yya[yyac] = new YYARec(261,305);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(308,115);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(258,-88 );yyac++; 
					yya[yyac] = new YYARec(260,307);yyac++; 
					yya[yyac] = new YYARec(261,308);yyac++; 
					yya[yyac] = new YYARec(258,309);yyac++; 
					yya[yyac] = new YYARec(258,310);yyac++; 
					yya[yyac] = new YYARec(275,311);yyac++; 
					yya[yyac] = new YYARec(284,237);yyac++; 
					yya[yyac] = new YYARec(285,238);yyac++; 
					yya[yyac] = new YYARec(286,239);yyac++; 
					yya[yyac] = new YYARec(258,-113 );yyac++; 
					yya[yyac] = new YYARec(266,-113 );yyac++; 
					yya[yyac] = new YYARec(269,-113 );yyac++; 
					yya[yyac] = new YYARec(270,-113 );yyac++; 
					yya[yyac] = new YYARec(271,-113 );yyac++; 
					yya[yyac] = new YYARec(272,-113 );yyac++; 
					yya[yyac] = new YYARec(273,-113 );yyac++; 
					yya[yyac] = new YYARec(275,-113 );yyac++; 
					yya[yyac] = new YYARec(276,-113 );yyac++; 
					yya[yyac] = new YYARec(277,-113 );yyac++; 
					yya[yyac] = new YYARec(278,-113 );yyac++; 
					yya[yyac] = new YYARec(279,-113 );yyac++; 
					yya[yyac] = new YYARec(280,-113 );yyac++; 
					yya[yyac] = new YYARec(281,-113 );yyac++; 
					yya[yyac] = new YYARec(282,-113 );yyac++; 
					yya[yyac] = new YYARec(283,-113 );yyac++; 
					yya[yyac] = new YYARec(282,241);yyac++; 
					yya[yyac] = new YYARec(283,242);yyac++; 
					yya[yyac] = new YYARec(258,-111 );yyac++; 
					yya[yyac] = new YYARec(266,-111 );yyac++; 
					yya[yyac] = new YYARec(269,-111 );yyac++; 
					yya[yyac] = new YYARec(270,-111 );yyac++; 
					yya[yyac] = new YYARec(271,-111 );yyac++; 
					yya[yyac] = new YYARec(272,-111 );yyac++; 
					yya[yyac] = new YYARec(273,-111 );yyac++; 
					yya[yyac] = new YYARec(275,-111 );yyac++; 
					yya[yyac] = new YYARec(276,-111 );yyac++; 
					yya[yyac] = new YYARec(277,-111 );yyac++; 
					yya[yyac] = new YYARec(278,-111 );yyac++; 
					yya[yyac] = new YYARec(279,-111 );yyac++; 
					yya[yyac] = new YYARec(280,-111 );yyac++; 
					yya[yyac] = new YYARec(281,-111 );yyac++; 
					yya[yyac] = new YYARec(278,244);yyac++; 
					yya[yyac] = new YYARec(279,245);yyac++; 
					yya[yyac] = new YYARec(280,246);yyac++; 
					yya[yyac] = new YYARec(281,247);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(266,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
					yya[yyac] = new YYARec(275,-109 );yyac++; 
					yya[yyac] = new YYARec(276,-109 );yyac++; 
					yya[yyac] = new YYARec(277,-109 );yyac++; 
					yya[yyac] = new YYARec(276,249);yyac++; 
					yya[yyac] = new YYARec(277,250);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(273,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(273,251);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(272,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(272,252);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(271,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(271,253);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(270,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(270,254);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(269,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(267,312);yyac++; 
					yya[yyac] = new YYARec(267,313);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,215);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(313,216);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(316,217);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(318,218);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(282,62);yyac++; 
					yya[yyac] = new YYARec(283,63);yyac++; 
					yya[yyac] = new YYARec(287,64);yyac++; 
					yya[yyac] = new YYARec(298,271);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(303,26);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,116);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,44);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(321,65);yyac++; 
					yya[yyac] = new YYARec(322,66);yyac++; 
					yya[yyac] = new YYARec(323,49);yyac++; 
					yya[yyac] = new YYARec(324,68);yyac++; 
					yya[yyac] = new YYARec(325,69);yyac++; 
					yya[yyac] = new YYARec(258,-62 );yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(305,215);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(313,216);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(316,217);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(318,218);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(260,319);yyac++; 
					yya[yyac] = new YYARec(261,320);yyac++; 
					yya[yyac] = new YYARec(258,321);yyac++; 
					yya[yyac] = new YYARec(258,322);yyac++; 
					yya[yyac] = new YYARec(258,323);yyac++; 
					yya[yyac] = new YYARec(258,324);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(259,130);yyac++; 
					yya[yyac] = new YYARec(266,131);yyac++; 
					yya[yyac] = new YYARec(288,132);yyac++; 
					yya[yyac] = new YYARec(294,133);yyac++; 
					yya[yyac] = new YYARec(295,134);yyac++; 
					yya[yyac] = new YYARec(296,135);yyac++; 
					yya[yyac] = new YYARec(298,114);yyac++; 
					yya[yyac] = new YYARec(299,22);yyac++; 
					yya[yyac] = new YYARec(300,38);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(304,39);yyac++; 
					yya[yyac] = new YYARec(305,40);yyac++; 
					yya[yyac] = new YYARec(306,41);yyac++; 
					yya[yyac] = new YYARec(307,42);yyac++; 
					yya[yyac] = new YYARec(309,43);yyac++; 
					yya[yyac] = new YYARec(310,136);yyac++; 
					yya[yyac] = new YYARec(312,28);yyac++; 
					yya[yyac] = new YYARec(313,29);yyac++; 
					yya[yyac] = new YYARec(314,137);yyac++; 
					yya[yyac] = new YYARec(315,45);yyac++; 
					yya[yyac] = new YYARec(316,117);yyac++; 
					yya[yyac] = new YYARec(317,46);yyac++; 
					yya[yyac] = new YYARec(318,47);yyac++; 
					yya[yyac] = new YYARec(319,118);yyac++; 
					yya[yyac] = new YYARec(320,48);yyac++; 
					yya[yyac] = new YYARec(323,138);yyac++; 
					yya[yyac] = new YYARec(261,-84 );yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(259,148);yyac++; 
					yya[yyac] = new YYARec(301,24);yyac++; 
					yya[yyac] = new YYARec(302,25);yyac++; 
					yya[yyac] = new YYARec(306,149);yyac++; 
					yya[yyac] = new YYARec(311,150);yyac++; 
					yya[yyac] = new YYARec(312,151);yyac++; 
					yya[yyac] = new YYARec(315,152);yyac++; 
					yya[yyac] = new YYARec(317,153);yyac++; 
					yya[yyac] = new YYARec(319,154);yyac++; 
					yya[yyac] = new YYARec(320,155);yyac++; 
					yya[yyac] = new YYARec(323,67);yyac++; 
					yya[yyac] = new YYARec(261,-60 );yyac++; 
					yya[yyac] = new YYARec(261,327);yyac++; 
					yya[yyac] = new YYARec(261,328);yyac++;

					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-30,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
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
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-47,33);yygc++; 
					yyg[yygc] = new YYARec(-39,34);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,36);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,50);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-35,51);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,52);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-76,54);yygc++; 
					yyg[yygc] = new YYARec(-71,55);yygc++; 
					yyg[yygc] = new YYARec(-37,56);yygc++; 
					yyg[yygc] = new YYARec(-31,57);yygc++; 
					yyg[yygc] = new YYARec(-26,58);yygc++; 
					yyg[yygc] = new YYARec(-25,59);yygc++; 
					yyg[yygc] = new YYARec(-23,60);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,70);yygc++; 
					yyg[yygc] = new YYARec(-29,71);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-30,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,72);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,75);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,76);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,77);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,78);yygc++; 
					yyg[yygc] = new YYARec(-25,79);yygc++; 
					yyg[yygc] = new YYARec(-33,80);yygc++; 
					yyg[yygc] = new YYARec(-33,82);yygc++; 
					yyg[yygc] = new YYARec(-33,84);yygc++; 
					yyg[yygc] = new YYARec(-33,88);yygc++; 
					yyg[yygc] = new YYARec(-23,97);yygc++; 
					yyg[yygc] = new YYARec(-25,99);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,70);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-30,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-13,101);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,102);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-30,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-13,103);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,102);yygc++; 
					yyg[yygc] = new YYARec(-76,54);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,55);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-37,56);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,108);yygc++; 
					yyg[yygc] = new YYARec(-26,109);yygc++; 
					yyg[yygc] = new YYARec(-25,110);yygc++; 
					yyg[yygc] = new YYARec(-24,111);yygc++; 
					yyg[yygc] = new YYARec(-23,112);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-20,113);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,127);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-41,140);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-19,145);yygc++; 
					yyg[yygc] = new YYARec(-17,146);yygc++; 
					yyg[yygc] = new YYARec(-33,156);yygc++; 
					yyg[yygc] = new YYARec(-33,164);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,165);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-74,168);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,175);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,176);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,177);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,178);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,193);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,194);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,199);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,201);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-33,203);yygc++; 
					yyg[yygc] = new YYARec(-41,140);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-19,206);yygc++; 
					yyg[yygc] = new YYARec(-17,146);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,207);yygc++; 
					yyg[yygc] = new YYARec(-22,73);yygc++; 
					yyg[yygc] = new YYARec(-21,74);yygc++; 
					yyg[yygc] = new YYARec(-12,208);yygc++; 
					yyg[yygc] = new YYARec(-71,209);yygc++; 
					yyg[yygc] = new YYARec(-37,210);yygc++; 
					yyg[yygc] = new YYARec(-36,211);yygc++; 
					yyg[yygc] = new YYARec(-44,213);yygc++; 
					yyg[yygc] = new YYARec(-41,214);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-44,219);yygc++; 
					yyg[yygc] = new YYARec(-41,220);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-76,54);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,55);yygc++; 
					yyg[yygc] = new YYARec(-54,221);yygc++; 
					yyg[yygc] = new YYARec(-53,222);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-37,56);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,223);yygc++; 
					yyg[yygc] = new YYARec(-26,224);yygc++; 
					yyg[yygc] = new YYARec(-25,225);yygc++; 
					yyg[yygc] = new YYARec(-24,226);yygc++; 
					yyg[yygc] = new YYARec(-23,227);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,228);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,229);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,235);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-69,236);yygc++; 
					yyg[yygc] = new YYARec(-67,240);yygc++; 
					yyg[yygc] = new YYARec(-65,243);yygc++; 
					yyg[yygc] = new YYARec(-63,248);yygc++; 
					yyg[yygc] = new YYARec(-74,256);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,257);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,258);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,261);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-76,54);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,55);yygc++; 
					yyg[yygc] = new YYARec(-46,262);yygc++; 
					yyg[yygc] = new YYARec(-45,263);yygc++; 
					yyg[yygc] = new YYARec(-43,264);yygc++; 
					yyg[yygc] = new YYARec(-42,265);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,266);yygc++; 
					yyg[yygc] = new YYARec(-37,56);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,267);yygc++; 
					yyg[yygc] = new YYARec(-26,268);yygc++; 
					yyg[yygc] = new YYARec(-25,269);yygc++; 
					yyg[yygc] = new YYARec(-23,270);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-41,140);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-19,272);yygc++; 
					yyg[yygc] = new YYARec(-17,146);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-30,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-11,7);yygc++; 
					yyg[yygc] = new YYARec(-10,8);yygc++; 
					yyg[yygc] = new YYARec(-9,9);yygc++; 
					yyg[yygc] = new YYARec(-8,10);yygc++; 
					yyg[yygc] = new YYARec(-7,11);yygc++; 
					yyg[yygc] = new YYARec(-6,12);yygc++; 
					yyg[yygc] = new YYARec(-5,13);yygc++; 
					yyg[yygc] = new YYARec(-4,14);yygc++; 
					yyg[yygc] = new YYARec(-3,277);yygc++; 
					yyg[yygc] = new YYARec(-33,278);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,279);yygc++; 
					yyg[yygc] = new YYARec(-15,280);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,279);yygc++; 
					yyg[yygc] = new YYARec(-15,281);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,282);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,283);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,284);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,285);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,286);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,287);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,288);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,289);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,290);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,291);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,292);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-73,179);yygc++; 
					yyg[yygc] = new YYARec(-72,180);yygc++; 
					yyg[yygc] = new YYARec(-71,181);yygc++; 
					yyg[yygc] = new YYARec(-70,182);yygc++; 
					yyg[yygc] = new YYARec(-68,183);yygc++; 
					yyg[yygc] = new YYARec(-66,184);yygc++; 
					yyg[yygc] = new YYARec(-64,185);yygc++; 
					yyg[yygc] = new YYARec(-62,186);yygc++; 
					yyg[yygc] = new YYARec(-61,187);yygc++; 
					yyg[yygc] = new YYARec(-60,188);yygc++; 
					yyg[yygc] = new YYARec(-59,189);yygc++; 
					yyg[yygc] = new YYARec(-58,190);yygc++; 
					yyg[yygc] = new YYARec(-57,191);yygc++; 
					yyg[yygc] = new YYARec(-56,192);yygc++; 
					yyg[yygc] = new YYARec(-55,293);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,200);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,296);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,297);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-33,299);yygc++; 
					yyg[yygc] = new YYARec(-41,140);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-19,301);yygc++; 
					yyg[yygc] = new YYARec(-18,302);yygc++; 
					yyg[yygc] = new YYARec(-17,146);yygc++; 
					yyg[yygc] = new YYARec(-41,140);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-19,301);yygc++; 
					yyg[yygc] = new YYARec(-18,303);yygc++; 
					yyg[yygc] = new YYARec(-17,146);yygc++; 
					yyg[yygc] = new YYARec(-71,209);yygc++; 
					yyg[yygc] = new YYARec(-37,210);yygc++; 
					yyg[yygc] = new YYARec(-36,304);yygc++; 
					yyg[yygc] = new YYARec(-76,54);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,55);yygc++; 
					yyg[yygc] = new YYARec(-54,221);yygc++; 
					yyg[yygc] = new YYARec(-53,306);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-37,56);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,106);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,223);yygc++; 
					yyg[yygc] = new YYARec(-26,224);yygc++; 
					yyg[yygc] = new YYARec(-25,225);yygc++; 
					yyg[yygc] = new YYARec(-24,226);yygc++; 
					yyg[yygc] = new YYARec(-23,227);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-69,236);yygc++; 
					yyg[yygc] = new YYARec(-67,240);yygc++; 
					yyg[yygc] = new YYARec(-65,243);yygc++; 
					yyg[yygc] = new YYARec(-63,248);yygc++; 
					yyg[yygc] = new YYARec(-44,314);yygc++; 
					yyg[yygc] = new YYARec(-41,315);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-76,54);yygc++; 
					yyg[yygc] = new YYARec(-75,31);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-71,55);yygc++; 
					yyg[yygc] = new YYARec(-46,262);yygc++; 
					yyg[yygc] = new YYARec(-45,263);yygc++; 
					yyg[yygc] = new YYARec(-43,264);yygc++; 
					yyg[yygc] = new YYARec(-42,316);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,266);yygc++; 
					yyg[yygc] = new YYARec(-37,56);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,267);yygc++; 
					yyg[yygc] = new YYARec(-26,268);yygc++; 
					yyg[yygc] = new YYARec(-25,269);yygc++; 
					yyg[yygc] = new YYARec(-23,270);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-44,317);yygc++; 
					yyg[yygc] = new YYARec(-41,318);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-75,119);yygc++; 
					yyg[yygc] = new YYARec(-72,32);yygc++; 
					yyg[yygc] = new YYARec(-52,120);yygc++; 
					yyg[yygc] = new YYARec(-51,121);yygc++; 
					yyg[yygc] = new YYARec(-50,122);yygc++; 
					yyg[yygc] = new YYARec(-49,123);yygc++; 
					yyg[yygc] = new YYARec(-48,124);yygc++; 
					yyg[yygc] = new YYARec(-45,104);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,105);yygc++; 
					yyg[yygc] = new YYARec(-34,35);yygc++; 
					yyg[yygc] = new YYARec(-32,125);yygc++; 
					yyg[yygc] = new YYARec(-28,107);yygc++; 
					yyg[yygc] = new YYARec(-27,126);yygc++; 
					yyg[yygc] = new YYARec(-21,37);yygc++; 
					yyg[yygc] = new YYARec(-16,325);yygc++; 
					yyg[yygc] = new YYARec(-14,128);yygc++; 
					yyg[yygc] = new YYARec(-41,140);yygc++; 
					yyg[yygc] = new YYARec(-40,141);yygc++; 
					yyg[yygc] = new YYARec(-39,142);yygc++; 
					yyg[yygc] = new YYARec(-34,143);yygc++; 
					yyg[yygc] = new YYARec(-22,144);yygc++; 
					yyg[yygc] = new YYARec(-19,326);yygc++; 
					yyg[yygc] = new YYARec(-17,146);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -57;  
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
					yyd[22] = -40;  
					yyd[23] = -38;  
					yyd[24] = -51;  
					yyd[25] = -55;  
					yyd[26] = -77;  
					yyd[27] = -36;  
					yyd[28] = -39;  
					yyd[29] = -56;  
					yyd[30] = -37;  
					yyd[31] = -200;  
					yyd[32] = -202;  
					yyd[33] = 0;  
					yyd[34] = -204;  
					yyd[35] = -201;  
					yyd[36] = -205;  
					yyd[37] = -203;  
					yyd[38] = -197;  
					yyd[39] = -146;  
					yyd[40] = -196;  
					yyd[41] = -199;  
					yyd[42] = -178;  
					yyd[43] = -192;  
					yyd[44] = -144;  
					yyd[45] = -145;  
					yyd[46] = -191;  
					yyd[47] = -176;  
					yyd[48] = -177;  
					yyd[49] = -198;  
					yyd[50] = 0;  
					yyd[51] = 0;  
					yyd[52] = -190;  
					yyd[53] = -189;  
					yyd[54] = -179;  
					yyd[55] = 0;  
					yyd[56] = -180;  
					yyd[57] = 0;  
					yyd[58] = -45;  
					yyd[59] = -46;  
					yyd[60] = -43;  
					yyd[61] = -44;  
					yyd[62] = -134;  
					yyd[63] = -135;  
					yyd[64] = -133;  
					yyd[65] = -184;  
					yyd[66] = -186;  
					yyd[67] = -187;  
					yyd[68] = -208;  
					yyd[69] = -209;  
					yyd[70] = 0;  
					yyd[71] = 0;  
					yyd[72] = -2;  
					yyd[73] = -27;  
					yyd[74] = -26;  
					yyd[75] = 0;  
					yyd[76] = 0;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = -95;  
					yyd[82] = 0;  
					yyd[83] = -54;  
					yyd[84] = 0;  
					yyd[85] = -183;  
					yyd[86] = -185;  
					yyd[87] = -35;  
					yyd[88] = 0;  
					yyd[89] = -34;  
					yyd[90] = 0;  
					yyd[91] = 0;  
					yyd[92] = -24;  
					yyd[93] = 0;  
					yyd[94] = -25;  
					yyd[95] = -33;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = -42;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = -156;  
					yyd[106] = 0;  
					yyd[107] = -155;  
					yyd[108] = -32;  
					yyd[109] = -31;  
					yyd[110] = -30;  
					yyd[111] = -29;  
					yyd[112] = -28;  
					yyd[113] = 0;  
					yyd[114] = -154;  
					yyd[115] = -188;  
					yyd[116] = -195;  
					yyd[117] = -194;  
					yyd[118] = -193;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = -85;  
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
					yyd[133] = 0;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = -53;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = -171;  
					yyd[143] = -172;  
					yyd[144] = -170;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = 0;  
					yyd[148] = 0;  
					yyd[149] = -169;  
					yyd[150] = -165;  
					yyd[151] = -164;  
					yyd[152] = -167;  
					yyd[153] = -168;  
					yyd[154] = -166;  
					yyd[155] = -163;  
					yyd[156] = 0;  
					yyd[157] = -11;  
					yyd[158] = 0;  
					yyd[159] = -14;  
					yyd[160] = -12;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = -23;  
					yyd[164] = 0;  
					yyd[165] = -83;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = -139;  
					yyd[170] = -140;  
					yyd[171] = -141;  
					yyd[172] = -142;  
					yyd[173] = -143;  
					yyd[174] = -76;  
					yyd[175] = -82;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = -120;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = -116;  
					yyd[183] = -114;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = 0;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = 0;  
					yyd[193] = -136;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = -182;  
					yyd[197] = -181;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = -121;  
					yyd[201] = 0;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = -52;  
					yyd[206] = -59;  
					yyd[207] = 0;  
					yyd[208] = 0;  
					yyd[209] = 0;  
					yyd[210] = 0;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = -151;  
					yyd[214] = -152;  
					yyd[215] = -162;  
					yyd[216] = -160;  
					yyd[217] = -161;  
					yyd[218] = -159;  
					yyd[219] = -150;  
					yyd[220] = -153;  
					yyd[221] = 0;  
					yyd[222] = -86;  
					yyd[223] = -94;  
					yyd[224] = -93;  
					yyd[225] = -92;  
					yyd[226] = -91;  
					yyd[227] = -90;  
					yyd[228] = -81;  
					yyd[229] = -80;  
					yyd[230] = -138;  
					yyd[231] = 0;  
					yyd[232] = 0;  
					yyd[233] = 0;  
					yyd[234] = 0;  
					yyd[235] = -117;  
					yyd[236] = 0;  
					yyd[237] = -130;  
					yyd[238] = -131;  
					yyd[239] = -132;  
					yyd[240] = 0;  
					yyd[241] = -128;  
					yyd[242] = -129;  
					yyd[243] = 0;  
					yyd[244] = -124;  
					yyd[245] = -125;  
					yyd[246] = -126;  
					yyd[247] = -127;  
					yyd[248] = 0;  
					yyd[249] = -122;  
					yyd[250] = -123;  
					yyd[251] = 0;  
					yyd[252] = 0;  
					yyd[253] = 0;  
					yyd[254] = 0;  
					yyd[255] = 0;  
					yyd[256] = 0;  
					yyd[257] = 0;  
					yyd[258] = 0;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = -78;  
					yyd[262] = -71;  
					yyd[263] = 0;  
					yyd[264] = 0;  
					yyd[265] = -61;  
					yyd[266] = -72;  
					yyd[267] = 0;  
					yyd[268] = -73;  
					yyd[269] = -70;  
					yyd[270] = -69;  
					yyd[271] = -68;  
					yyd[272] = -58;  
					yyd[273] = 0;  
					yyd[274] = 0;  
					yyd[275] = 0;  
					yyd[276] = -47;  
					yyd[277] = 0;  
					yyd[278] = 0;  
					yyd[279] = 0;  
					yyd[280] = 0;  
					yyd[281] = 0;  
					yyd[282] = -79;  
					yyd[283] = 0;  
					yyd[284] = -115;  
					yyd[285] = 0;  
					yyd[286] = 0;  
					yyd[287] = 0;  
					yyd[288] = 0;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = 0;  
					yyd[292] = 0;  
					yyd[293] = -137;  
					yyd[294] = -119;  
					yyd[295] = -147;  
					yyd[296] = 0;  
					yyd[297] = 0;  
					yyd[298] = 0;  
					yyd[299] = 0;  
					yyd[300] = 0;  
					yyd[301] = 0;  
					yyd[302] = 0;  
					yyd[303] = 0;  
					yyd[304] = -48;  
					yyd[305] = -13;  
					yyd[306] = -89;  
					yyd[307] = 0;  
					yyd[308] = -18;  
					yyd[309] = -15;  
					yyd[310] = -16;  
					yyd[311] = -118;  
					yyd[312] = -148;  
					yyd[313] = -149;  
					yyd[314] = -66;  
					yyd[315] = -65;  
					yyd[316] = -63;  
					yyd[317] = -64;  
					yyd[318] = -67;  
					yyd[319] = 0;  
					yyd[320] = -22;  
					yyd[321] = -19;  
					yyd[322] = -20;  
					yyd[323] = 0;  
					yyd[324] = 0;  
					yyd[325] = 0;  
					yyd[326] = 0;  
					yyd[327] = -17;  
					yyd[328] = -21; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 16;  
					yyal[2] = 30;  
					yyal[3] = 30;  
					yyal[4] = 43;  
					yyal[5] = 57;  
					yyal[6] = 65;  
					yyal[7] = 78;  
					yyal[8] = 78;  
					yyal[9] = 78;  
					yyal[10] = 78;  
					yyal[11] = 78;  
					yyal[12] = 78;  
					yyal[13] = 78;  
					yyal[14] = 78;  
					yyal[15] = 95;  
					yyal[16] = 95;  
					yyal[17] = 96;  
					yyal[18] = 99;  
					yyal[19] = 102;  
					yyal[20] = 105;  
					yyal[21] = 108;  
					yyal[22] = 109;  
					yyal[23] = 109;  
					yyal[24] = 109;  
					yyal[25] = 109;  
					yyal[26] = 109;  
					yyal[27] = 109;  
					yyal[28] = 109;  
					yyal[29] = 109;  
					yyal[30] = 109;  
					yyal[31] = 109;  
					yyal[32] = 109;  
					yyal[33] = 109;  
					yyal[34] = 111;  
					yyal[35] = 111;  
					yyal[36] = 111;  
					yyal[37] = 111;  
					yyal[38] = 111;  
					yyal[39] = 111;  
					yyal[40] = 111;  
					yyal[41] = 111;  
					yyal[42] = 111;  
					yyal[43] = 111;  
					yyal[44] = 111;  
					yyal[45] = 111;  
					yyal[46] = 111;  
					yyal[47] = 111;  
					yyal[48] = 111;  
					yyal[49] = 111;  
					yyal[50] = 111;  
					yyal[51] = 115;  
					yyal[52] = 117;  
					yyal[53] = 117;  
					yyal[54] = 117;  
					yyal[55] = 117;  
					yyal[56] = 119;  
					yyal[57] = 119;  
					yyal[58] = 120;  
					yyal[59] = 120;  
					yyal[60] = 120;  
					yyal[61] = 120;  
					yyal[62] = 120;  
					yyal[63] = 120;  
					yyal[64] = 120;  
					yyal[65] = 120;  
					yyal[66] = 120;  
					yyal[67] = 120;  
					yyal[68] = 120;  
					yyal[69] = 120;  
					yyal[70] = 120;  
					yyal[71] = 135;  
					yyal[72] = 136;  
					yyal[73] = 136;  
					yyal[74] = 136;  
					yyal[75] = 136;  
					yyal[76] = 137;  
					yyal[77] = 138;  
					yyal[78] = 140;  
					yyal[79] = 141;  
					yyal[80] = 142;  
					yyal[81] = 143;  
					yyal[82] = 143;  
					yyal[83] = 145;  
					yyal[84] = 145;  
					yyal[85] = 146;  
					yyal[86] = 146;  
					yyal[87] = 146;  
					yyal[88] = 146;  
					yyal[89] = 160;  
					yyal[90] = 160;  
					yyal[91] = 176;  
					yyal[92] = 192;  
					yyal[93] = 192;  
					yyal[94] = 221;  
					yyal[95] = 221;  
					yyal[96] = 221;  
					yyal[97] = 250;  
					yyal[98] = 251;  
					yyal[99] = 264;  
					yyal[100] = 270;  
					yyal[101] = 270;  
					yyal[102] = 271;  
					yyal[103] = 273;  
					yyal[104] = 274;  
					yyal[105] = 327;  
					yyal[106] = 327;  
					yyal[107] = 380;  
					yyal[108] = 380;  
					yyal[109] = 380;  
					yyal[110] = 380;  
					yyal[111] = 380;  
					yyal[112] = 380;  
					yyal[113] = 380;  
					yyal[114] = 381;  
					yyal[115] = 381;  
					yyal[116] = 381;  
					yyal[117] = 381;  
					yyal[118] = 381;  
					yyal[119] = 381;  
					yyal[120] = 419;  
					yyal[121] = 450;  
					yyal[122] = 450;  
					yyal[123] = 481;  
					yyal[124] = 482;  
					yyal[125] = 483;  
					yyal[126] = 490;  
					yyal[127] = 495;  
					yyal[128] = 496;  
					yyal[129] = 527;  
					yyal[130] = 530;  
					yyal[131] = 533;  
					yyal[132] = 562;  
					yyal[133] = 589;  
					yyal[134] = 621;  
					yyal[135] = 648;  
					yyal[136] = 675;  
					yyal[137] = 682;  
					yyal[138] = 720;  
					yyal[139] = 728;  
					yyal[140] = 728;  
					yyal[141] = 756;  
					yyal[142] = 757;  
					yyal[143] = 757;  
					yyal[144] = 757;  
					yyal[145] = 757;  
					yyal[146] = 758;  
					yyal[147] = 773;  
					yyal[148] = 776;  
					yyal[149] = 779;  
					yyal[150] = 779;  
					yyal[151] = 779;  
					yyal[152] = 779;  
					yyal[153] = 779;  
					yyal[154] = 779;  
					yyal[155] = 779;  
					yyal[156] = 779;  
					yyal[157] = 784;  
					yyal[158] = 784;  
					yyal[159] = 785;  
					yyal[160] = 785;  
					yyal[161] = 785;  
					yyal[162] = 799;  
					yyal[163] = 813;  
					yyal[164] = 813;  
					yyal[165] = 842;  
					yyal[166] = 842;  
					yyal[167] = 873;  
					yyal[168] = 904;  
					yyal[169] = 931;  
					yyal[170] = 931;  
					yyal[171] = 931;  
					yyal[172] = 931;  
					yyal[173] = 931;  
					yyal[174] = 931;  
					yyal[175] = 931;  
					yyal[176] = 931;  
					yyal[177] = 932;  
					yyal[178] = 933;  
					yyal[179] = 934;  
					yyal[180] = 934;  
					yyal[181] = 960;  
					yyal[182] = 987;  
					yyal[183] = 987;  
					yyal[184] = 987;  
					yyal[185] = 1006;  
					yyal[186] = 1022;  
					yyal[187] = 1036;  
					yyal[188] = 1046;  
					yyal[189] = 1054;  
					yyal[190] = 1061;  
					yyal[191] = 1067;  
					yyal[192] = 1072;  
					yyal[193] = 1076;  
					yyal[194] = 1076;  
					yyal[195] = 1098;  
					yyal[196] = 1125;  
					yyal[197] = 1125;  
					yyal[198] = 1125;  
					yyal[199] = 1154;  
					yyal[200] = 1155;  
					yyal[201] = 1155;  
					yyal[202] = 1156;  
					yyal[203] = 1187;  
					yyal[204] = 1214;  
					yyal[205] = 1229;  
					yyal[206] = 1229;  
					yyal[207] = 1229;  
					yyal[208] = 1230;  
					yyal[209] = 1231;  
					yyal[210] = 1232;  
					yyal[211] = 1234;  
					yyal[212] = 1235;  
					yyal[213] = 1250;  
					yyal[214] = 1250;  
					yyal[215] = 1250;  
					yyal[216] = 1250;  
					yyal[217] = 1250;  
					yyal[218] = 1250;  
					yyal[219] = 1250;  
					yyal[220] = 1250;  
					yyal[221] = 1250;  
					yyal[222] = 1281;  
					yyal[223] = 1281;  
					yyal[224] = 1281;  
					yyal[225] = 1281;  
					yyal[226] = 1281;  
					yyal[227] = 1281;  
					yyal[228] = 1281;  
					yyal[229] = 1281;  
					yyal[230] = 1281;  
					yyal[231] = 1281;  
					yyal[232] = 1311;  
					yyal[233] = 1341;  
					yyal[234] = 1372;  
					yyal[235] = 1399;  
					yyal[236] = 1399;  
					yyal[237] = 1426;  
					yyal[238] = 1426;  
					yyal[239] = 1426;  
					yyal[240] = 1426;  
					yyal[241] = 1453;  
					yyal[242] = 1453;  
					yyal[243] = 1453;  
					yyal[244] = 1480;  
					yyal[245] = 1480;  
					yyal[246] = 1480;  
					yyal[247] = 1480;  
					yyal[248] = 1480;  
					yyal[249] = 1507;  
					yyal[250] = 1507;  
					yyal[251] = 1507;  
					yyal[252] = 1534;  
					yyal[253] = 1561;  
					yyal[254] = 1588;  
					yyal[255] = 1615;  
					yyal[256] = 1642;  
					yyal[257] = 1669;  
					yyal[258] = 1670;  
					yyal[259] = 1671;  
					yyal[260] = 1700;  
					yyal[261] = 1729;  
					yyal[262] = 1729;  
					yyal[263] = 1729;  
					yyal[264] = 1759;  
					yyal[265] = 1788;  
					yyal[266] = 1788;  
					yyal[267] = 1788;  
					yyal[268] = 1818;  
					yyal[269] = 1818;  
					yyal[270] = 1818;  
					yyal[271] = 1818;  
					yyal[272] = 1818;  
					yyal[273] = 1818;  
					yyal[274] = 1832;  
					yyal[275] = 1846;  
					yyal[276] = 1851;  
					yyal[277] = 1851;  
					yyal[278] = 1852;  
					yyal[279] = 1882;  
					yyal[280] = 1884;  
					yyal[281] = 1885;  
					yyal[282] = 1886;  
					yyal[283] = 1886;  
					yyal[284] = 1887;  
					yyal[285] = 1887;  
					yyal[286] = 1906;  
					yyal[287] = 1922;  
					yyal[288] = 1936;  
					yyal[289] = 1946;  
					yyal[290] = 1954;  
					yyal[291] = 1961;  
					yyal[292] = 1967;  
					yyal[293] = 1972;  
					yyal[294] = 1972;  
					yyal[295] = 1972;  
					yyal[296] = 1972;  
					yyal[297] = 1973;  
					yyal[298] = 1974;  
					yyal[299] = 1988;  
					yyal[300] = 2016;  
					yyal[301] = 2030;  
					yyal[302] = 2032;  
					yyal[303] = 2033;  
					yyal[304] = 2034;  
					yyal[305] = 2034;  
					yyal[306] = 2034;  
					yyal[307] = 2034;  
					yyal[308] = 2035;  
					yyal[309] = 2035;  
					yyal[310] = 2035;  
					yyal[311] = 2035;  
					yyal[312] = 2035;  
					yyal[313] = 2035;  
					yyal[314] = 2035;  
					yyal[315] = 2035;  
					yyal[316] = 2035;  
					yyal[317] = 2035;  
					yyal[318] = 2035;  
					yyal[319] = 2035;  
					yyal[320] = 2036;  
					yyal[321] = 2036;  
					yyal[322] = 2036;  
					yyal[323] = 2036;  
					yyal[324] = 2065;  
					yyal[325] = 2078;  
					yyal[326] = 2079;  
					yyal[327] = 2080;  
					yyal[328] = 2080; 

					yyah = new int[yynstates];
					yyah[0] = 15;  
					yyah[1] = 29;  
					yyah[2] = 29;  
					yyah[3] = 42;  
					yyah[4] = 56;  
					yyah[5] = 64;  
					yyah[6] = 77;  
					yyah[7] = 77;  
					yyah[8] = 77;  
					yyah[9] = 77;  
					yyah[10] = 77;  
					yyah[11] = 77;  
					yyah[12] = 77;  
					yyah[13] = 77;  
					yyah[14] = 94;  
					yyah[15] = 94;  
					yyah[16] = 95;  
					yyah[17] = 98;  
					yyah[18] = 101;  
					yyah[19] = 104;  
					yyah[20] = 107;  
					yyah[21] = 108;  
					yyah[22] = 108;  
					yyah[23] = 108;  
					yyah[24] = 108;  
					yyah[25] = 108;  
					yyah[26] = 108;  
					yyah[27] = 108;  
					yyah[28] = 108;  
					yyah[29] = 108;  
					yyah[30] = 108;  
					yyah[31] = 108;  
					yyah[32] = 108;  
					yyah[33] = 110;  
					yyah[34] = 110;  
					yyah[35] = 110;  
					yyah[36] = 110;  
					yyah[37] = 110;  
					yyah[38] = 110;  
					yyah[39] = 110;  
					yyah[40] = 110;  
					yyah[41] = 110;  
					yyah[42] = 110;  
					yyah[43] = 110;  
					yyah[44] = 110;  
					yyah[45] = 110;  
					yyah[46] = 110;  
					yyah[47] = 110;  
					yyah[48] = 110;  
					yyah[49] = 110;  
					yyah[50] = 114;  
					yyah[51] = 116;  
					yyah[52] = 116;  
					yyah[53] = 116;  
					yyah[54] = 116;  
					yyah[55] = 118;  
					yyah[56] = 118;  
					yyah[57] = 119;  
					yyah[58] = 119;  
					yyah[59] = 119;  
					yyah[60] = 119;  
					yyah[61] = 119;  
					yyah[62] = 119;  
					yyah[63] = 119;  
					yyah[64] = 119;  
					yyah[65] = 119;  
					yyah[66] = 119;  
					yyah[67] = 119;  
					yyah[68] = 119;  
					yyah[69] = 119;  
					yyah[70] = 134;  
					yyah[71] = 135;  
					yyah[72] = 135;  
					yyah[73] = 135;  
					yyah[74] = 135;  
					yyah[75] = 136;  
					yyah[76] = 137;  
					yyah[77] = 139;  
					yyah[78] = 140;  
					yyah[79] = 141;  
					yyah[80] = 142;  
					yyah[81] = 142;  
					yyah[82] = 144;  
					yyah[83] = 144;  
					yyah[84] = 145;  
					yyah[85] = 145;  
					yyah[86] = 145;  
					yyah[87] = 145;  
					yyah[88] = 159;  
					yyah[89] = 159;  
					yyah[90] = 175;  
					yyah[91] = 191;  
					yyah[92] = 191;  
					yyah[93] = 220;  
					yyah[94] = 220;  
					yyah[95] = 220;  
					yyah[96] = 249;  
					yyah[97] = 250;  
					yyah[98] = 263;  
					yyah[99] = 269;  
					yyah[100] = 269;  
					yyah[101] = 270;  
					yyah[102] = 272;  
					yyah[103] = 273;  
					yyah[104] = 326;  
					yyah[105] = 326;  
					yyah[106] = 379;  
					yyah[107] = 379;  
					yyah[108] = 379;  
					yyah[109] = 379;  
					yyah[110] = 379;  
					yyah[111] = 379;  
					yyah[112] = 379;  
					yyah[113] = 380;  
					yyah[114] = 380;  
					yyah[115] = 380;  
					yyah[116] = 380;  
					yyah[117] = 380;  
					yyah[118] = 380;  
					yyah[119] = 418;  
					yyah[120] = 449;  
					yyah[121] = 449;  
					yyah[122] = 480;  
					yyah[123] = 481;  
					yyah[124] = 482;  
					yyah[125] = 489;  
					yyah[126] = 494;  
					yyah[127] = 495;  
					yyah[128] = 526;  
					yyah[129] = 529;  
					yyah[130] = 532;  
					yyah[131] = 561;  
					yyah[132] = 588;  
					yyah[133] = 620;  
					yyah[134] = 647;  
					yyah[135] = 674;  
					yyah[136] = 681;  
					yyah[137] = 719;  
					yyah[138] = 727;  
					yyah[139] = 727;  
					yyah[140] = 755;  
					yyah[141] = 756;  
					yyah[142] = 756;  
					yyah[143] = 756;  
					yyah[144] = 756;  
					yyah[145] = 757;  
					yyah[146] = 772;  
					yyah[147] = 775;  
					yyah[148] = 778;  
					yyah[149] = 778;  
					yyah[150] = 778;  
					yyah[151] = 778;  
					yyah[152] = 778;  
					yyah[153] = 778;  
					yyah[154] = 778;  
					yyah[155] = 778;  
					yyah[156] = 783;  
					yyah[157] = 783;  
					yyah[158] = 784;  
					yyah[159] = 784;  
					yyah[160] = 784;  
					yyah[161] = 798;  
					yyah[162] = 812;  
					yyah[163] = 812;  
					yyah[164] = 841;  
					yyah[165] = 841;  
					yyah[166] = 872;  
					yyah[167] = 903;  
					yyah[168] = 930;  
					yyah[169] = 930;  
					yyah[170] = 930;  
					yyah[171] = 930;  
					yyah[172] = 930;  
					yyah[173] = 930;  
					yyah[174] = 930;  
					yyah[175] = 930;  
					yyah[176] = 931;  
					yyah[177] = 932;  
					yyah[178] = 933;  
					yyah[179] = 933;  
					yyah[180] = 959;  
					yyah[181] = 986;  
					yyah[182] = 986;  
					yyah[183] = 986;  
					yyah[184] = 1005;  
					yyah[185] = 1021;  
					yyah[186] = 1035;  
					yyah[187] = 1045;  
					yyah[188] = 1053;  
					yyah[189] = 1060;  
					yyah[190] = 1066;  
					yyah[191] = 1071;  
					yyah[192] = 1075;  
					yyah[193] = 1075;  
					yyah[194] = 1097;  
					yyah[195] = 1124;  
					yyah[196] = 1124;  
					yyah[197] = 1124;  
					yyah[198] = 1153;  
					yyah[199] = 1154;  
					yyah[200] = 1154;  
					yyah[201] = 1155;  
					yyah[202] = 1186;  
					yyah[203] = 1213;  
					yyah[204] = 1228;  
					yyah[205] = 1228;  
					yyah[206] = 1228;  
					yyah[207] = 1229;  
					yyah[208] = 1230;  
					yyah[209] = 1231;  
					yyah[210] = 1233;  
					yyah[211] = 1234;  
					yyah[212] = 1249;  
					yyah[213] = 1249;  
					yyah[214] = 1249;  
					yyah[215] = 1249;  
					yyah[216] = 1249;  
					yyah[217] = 1249;  
					yyah[218] = 1249;  
					yyah[219] = 1249;  
					yyah[220] = 1249;  
					yyah[221] = 1280;  
					yyah[222] = 1280;  
					yyah[223] = 1280;  
					yyah[224] = 1280;  
					yyah[225] = 1280;  
					yyah[226] = 1280;  
					yyah[227] = 1280;  
					yyah[228] = 1280;  
					yyah[229] = 1280;  
					yyah[230] = 1280;  
					yyah[231] = 1310;  
					yyah[232] = 1340;  
					yyah[233] = 1371;  
					yyah[234] = 1398;  
					yyah[235] = 1398;  
					yyah[236] = 1425;  
					yyah[237] = 1425;  
					yyah[238] = 1425;  
					yyah[239] = 1425;  
					yyah[240] = 1452;  
					yyah[241] = 1452;  
					yyah[242] = 1452;  
					yyah[243] = 1479;  
					yyah[244] = 1479;  
					yyah[245] = 1479;  
					yyah[246] = 1479;  
					yyah[247] = 1479;  
					yyah[248] = 1506;  
					yyah[249] = 1506;  
					yyah[250] = 1506;  
					yyah[251] = 1533;  
					yyah[252] = 1560;  
					yyah[253] = 1587;  
					yyah[254] = 1614;  
					yyah[255] = 1641;  
					yyah[256] = 1668;  
					yyah[257] = 1669;  
					yyah[258] = 1670;  
					yyah[259] = 1699;  
					yyah[260] = 1728;  
					yyah[261] = 1728;  
					yyah[262] = 1728;  
					yyah[263] = 1758;  
					yyah[264] = 1787;  
					yyah[265] = 1787;  
					yyah[266] = 1787;  
					yyah[267] = 1817;  
					yyah[268] = 1817;  
					yyah[269] = 1817;  
					yyah[270] = 1817;  
					yyah[271] = 1817;  
					yyah[272] = 1817;  
					yyah[273] = 1831;  
					yyah[274] = 1845;  
					yyah[275] = 1850;  
					yyah[276] = 1850;  
					yyah[277] = 1851;  
					yyah[278] = 1881;  
					yyah[279] = 1883;  
					yyah[280] = 1884;  
					yyah[281] = 1885;  
					yyah[282] = 1885;  
					yyah[283] = 1886;  
					yyah[284] = 1886;  
					yyah[285] = 1905;  
					yyah[286] = 1921;  
					yyah[287] = 1935;  
					yyah[288] = 1945;  
					yyah[289] = 1953;  
					yyah[290] = 1960;  
					yyah[291] = 1966;  
					yyah[292] = 1971;  
					yyah[293] = 1971;  
					yyah[294] = 1971;  
					yyah[295] = 1971;  
					yyah[296] = 1972;  
					yyah[297] = 1973;  
					yyah[298] = 1987;  
					yyah[299] = 2015;  
					yyah[300] = 2029;  
					yyah[301] = 2031;  
					yyah[302] = 2032;  
					yyah[303] = 2033;  
					yyah[304] = 2033;  
					yyah[305] = 2033;  
					yyah[306] = 2033;  
					yyah[307] = 2034;  
					yyah[308] = 2034;  
					yyah[309] = 2034;  
					yyah[310] = 2034;  
					yyah[311] = 2034;  
					yyah[312] = 2034;  
					yyah[313] = 2034;  
					yyah[314] = 2034;  
					yyah[315] = 2034;  
					yyah[316] = 2034;  
					yyah[317] = 2034;  
					yyah[318] = 2034;  
					yyah[319] = 2035;  
					yyah[320] = 2035;  
					yyah[321] = 2035;  
					yyah[322] = 2035;  
					yyah[323] = 2064;  
					yyah[324] = 2077;  
					yyah[325] = 2078;  
					yyah[326] = 2079;  
					yyah[327] = 2079;  
					yyah[328] = 2079; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 17;  
					yygl[2] = 24;  
					yygl[3] = 24;  
					yygl[4] = 29;  
					yygl[5] = 35;  
					yygl[6] = 43;  
					yygl[7] = 49;  
					yygl[8] = 49;  
					yygl[9] = 49;  
					yygl[10] = 49;  
					yygl[11] = 49;  
					yygl[12] = 49;  
					yygl[13] = 49;  
					yygl[14] = 49;  
					yygl[15] = 64;  
					yygl[16] = 64;  
					yygl[17] = 64;  
					yygl[18] = 67;  
					yygl[19] = 70;  
					yygl[20] = 73;  
					yygl[21] = 76;  
					yygl[22] = 77;  
					yygl[23] = 77;  
					yygl[24] = 77;  
					yygl[25] = 77;  
					yygl[26] = 77;  
					yygl[27] = 77;  
					yygl[28] = 77;  
					yygl[29] = 77;  
					yygl[30] = 77;  
					yygl[31] = 77;  
					yygl[32] = 77;  
					yygl[33] = 77;  
					yygl[34] = 78;  
					yygl[35] = 78;  
					yygl[36] = 78;  
					yygl[37] = 78;  
					yygl[38] = 78;  
					yygl[39] = 78;  
					yygl[40] = 78;  
					yygl[41] = 78;  
					yygl[42] = 78;  
					yygl[43] = 78;  
					yygl[44] = 78;  
					yygl[45] = 78;  
					yygl[46] = 78;  
					yygl[47] = 78;  
					yygl[48] = 78;  
					yygl[49] = 78;  
					yygl[50] = 78;  
					yygl[51] = 79;  
					yygl[52] = 80;  
					yygl[53] = 80;  
					yygl[54] = 80;  
					yygl[55] = 80;  
					yygl[56] = 80;  
					yygl[57] = 80;  
					yygl[58] = 80;  
					yygl[59] = 80;  
					yygl[60] = 80;  
					yygl[61] = 80;  
					yygl[62] = 80;  
					yygl[63] = 80;  
					yygl[64] = 80;  
					yygl[65] = 80;  
					yygl[66] = 80;  
					yygl[67] = 80;  
					yygl[68] = 80;  
					yygl[69] = 80;  
					yygl[70] = 80;  
					yygl[71] = 81;  
					yygl[72] = 81;  
					yygl[73] = 81;  
					yygl[74] = 81;  
					yygl[75] = 81;  
					yygl[76] = 81;  
					yygl[77] = 81;  
					yygl[78] = 81;  
					yygl[79] = 81;  
					yygl[80] = 81;  
					yygl[81] = 81;  
					yygl[82] = 81;  
					yygl[83] = 82;  
					yygl[84] = 82;  
					yygl[85] = 83;  
					yygl[86] = 83;  
					yygl[87] = 83;  
					yygl[88] = 83;  
					yygl[89] = 89;  
					yygl[90] = 89;  
					yygl[91] = 105;  
					yygl[92] = 121;  
					yygl[93] = 121;  
					yygl[94] = 139;  
					yygl[95] = 139;  
					yygl[96] = 139;  
					yygl[97] = 156;  
					yygl[98] = 156;  
					yygl[99] = 163;  
					yygl[100] = 164;  
					yygl[101] = 164;  
					yygl[102] = 164;  
					yygl[103] = 164;  
					yygl[104] = 164;  
					yygl[105] = 164;  
					yygl[106] = 164;  
					yygl[107] = 164;  
					yygl[108] = 164;  
					yygl[109] = 164;  
					yygl[110] = 164;  
					yygl[111] = 164;  
					yygl[112] = 164;  
					yygl[113] = 164;  
					yygl[114] = 164;  
					yygl[115] = 164;  
					yygl[116] = 164;  
					yygl[117] = 164;  
					yygl[118] = 164;  
					yygl[119] = 164;  
					yygl[120] = 164;  
					yygl[121] = 165;  
					yygl[122] = 165;  
					yygl[123] = 182;  
					yygl[124] = 182;  
					yygl[125] = 182;  
					yygl[126] = 182;  
					yygl[127] = 183;  
					yygl[128] = 183;  
					yygl[129] = 200;  
					yygl[130] = 203;  
					yygl[131] = 206;  
					yygl[132] = 223;  
					yygl[133] = 247;  
					yygl[134] = 247;  
					yygl[135] = 271;  
					yygl[136] = 295;  
					yygl[137] = 295;  
					yygl[138] = 295;  
					yygl[139] = 295;  
					yygl[140] = 295;  
					yygl[141] = 296;  
					yygl[142] = 296;  
					yygl[143] = 296;  
					yygl[144] = 296;  
					yygl[145] = 296;  
					yygl[146] = 296;  
					yygl[147] = 303;  
					yygl[148] = 306;  
					yygl[149] = 309;  
					yygl[150] = 309;  
					yygl[151] = 309;  
					yygl[152] = 309;  
					yygl[153] = 309;  
					yygl[154] = 309;  
					yygl[155] = 309;  
					yygl[156] = 309;  
					yygl[157] = 312;  
					yygl[158] = 312;  
					yygl[159] = 312;  
					yygl[160] = 312;  
					yygl[161] = 312;  
					yygl[162] = 317;  
					yygl[163] = 322;  
					yygl[164] = 322;  
					yygl[165] = 341;  
					yygl[166] = 341;  
					yygl[167] = 358;  
					yygl[168] = 375;  
					yygl[169] = 399;  
					yygl[170] = 399;  
					yygl[171] = 399;  
					yygl[172] = 399;  
					yygl[173] = 399;  
					yygl[174] = 399;  
					yygl[175] = 399;  
					yygl[176] = 399;  
					yygl[177] = 399;  
					yygl[178] = 399;  
					yygl[179] = 399;  
					yygl[180] = 399;  
					yygl[181] = 399;  
					yygl[182] = 413;  
					yygl[183] = 413;  
					yygl[184] = 413;  
					yygl[185] = 414;  
					yygl[186] = 415;  
					yygl[187] = 416;  
					yygl[188] = 417;  
					yygl[189] = 417;  
					yygl[190] = 417;  
					yygl[191] = 417;  
					yygl[192] = 417;  
					yygl[193] = 417;  
					yygl[194] = 417;  
					yygl[195] = 418;  
					yygl[196] = 442;  
					yygl[197] = 442;  
					yygl[198] = 442;  
					yygl[199] = 459;  
					yygl[200] = 459;  
					yygl[201] = 459;  
					yygl[202] = 459;  
					yygl[203] = 476;  
					yygl[204] = 493;  
					yygl[205] = 500;  
					yygl[206] = 500;  
					yygl[207] = 500;  
					yygl[208] = 500;  
					yygl[209] = 500;  
					yygl[210] = 500;  
					yygl[211] = 500;  
					yygl[212] = 500;  
					yygl[213] = 515;  
					yygl[214] = 515;  
					yygl[215] = 515;  
					yygl[216] = 515;  
					yygl[217] = 515;  
					yygl[218] = 515;  
					yygl[219] = 515;  
					yygl[220] = 515;  
					yygl[221] = 515;  
					yygl[222] = 516;  
					yygl[223] = 516;  
					yygl[224] = 516;  
					yygl[225] = 516;  
					yygl[226] = 516;  
					yygl[227] = 516;  
					yygl[228] = 516;  
					yygl[229] = 516;  
					yygl[230] = 516;  
					yygl[231] = 516;  
					yygl[232] = 534;  
					yygl[233] = 552;  
					yygl[234] = 569;  
					yygl[235] = 593;  
					yygl[236] = 593;  
					yygl[237] = 607;  
					yygl[238] = 607;  
					yygl[239] = 607;  
					yygl[240] = 607;  
					yygl[241] = 622;  
					yygl[242] = 622;  
					yygl[243] = 622;  
					yygl[244] = 638;  
					yygl[245] = 638;  
					yygl[246] = 638;  
					yygl[247] = 638;  
					yygl[248] = 638;  
					yygl[249] = 655;  
					yygl[250] = 655;  
					yygl[251] = 655;  
					yygl[252] = 673;  
					yygl[253] = 692;  
					yygl[254] = 712;  
					yygl[255] = 733;  
					yygl[256] = 755;  
					yygl[257] = 779;  
					yygl[258] = 779;  
					yygl[259] = 779;  
					yygl[260] = 796;  
					yygl[261] = 813;  
					yygl[262] = 813;  
					yygl[263] = 813;  
					yygl[264] = 813;  
					yygl[265] = 814;  
					yygl[266] = 814;  
					yygl[267] = 814;  
					yygl[268] = 814;  
					yygl[269] = 814;  
					yygl[270] = 814;  
					yygl[271] = 814;  
					yygl[272] = 814;  
					yygl[273] = 814;  
					yygl[274] = 822;  
					yygl[275] = 830;  
					yygl[276] = 833;  
					yygl[277] = 833;  
					yygl[278] = 833;  
					yygl[279] = 852;  
					yygl[280] = 852;  
					yygl[281] = 852;  
					yygl[282] = 852;  
					yygl[283] = 852;  
					yygl[284] = 852;  
					yygl[285] = 852;  
					yygl[286] = 853;  
					yygl[287] = 854;  
					yygl[288] = 855;  
					yygl[289] = 856;  
					yygl[290] = 856;  
					yygl[291] = 856;  
					yygl[292] = 856;  
					yygl[293] = 856;  
					yygl[294] = 856;  
					yygl[295] = 856;  
					yygl[296] = 856;  
					yygl[297] = 856;  
					yygl[298] = 856;  
					yygl[299] = 861;  
					yygl[300] = 878;  
					yygl[301] = 883;  
					yygl[302] = 883;  
					yygl[303] = 883;  
					yygl[304] = 883;  
					yygl[305] = 883;  
					yygl[306] = 883;  
					yygl[307] = 883;  
					yygl[308] = 883;  
					yygl[309] = 883;  
					yygl[310] = 883;  
					yygl[311] = 883;  
					yygl[312] = 883;  
					yygl[313] = 883;  
					yygl[314] = 883;  
					yygl[315] = 883;  
					yygl[316] = 883;  
					yygl[317] = 883;  
					yygl[318] = 883;  
					yygl[319] = 883;  
					yygl[320] = 883;  
					yygl[321] = 883;  
					yygl[322] = 883;  
					yygl[323] = 883;  
					yygl[324] = 900;  
					yygl[325] = 907;  
					yygl[326] = 907;  
					yygl[327] = 907;  
					yygl[328] = 907; 

					yygh = new int[yynstates];
					yygh[0] = 16;  
					yygh[1] = 23;  
					yygh[2] = 23;  
					yygh[3] = 28;  
					yygh[4] = 34;  
					yygh[5] = 42;  
					yygh[6] = 48;  
					yygh[7] = 48;  
					yygh[8] = 48;  
					yygh[9] = 48;  
					yygh[10] = 48;  
					yygh[11] = 48;  
					yygh[12] = 48;  
					yygh[13] = 48;  
					yygh[14] = 63;  
					yygh[15] = 63;  
					yygh[16] = 63;  
					yygh[17] = 66;  
					yygh[18] = 69;  
					yygh[19] = 72;  
					yygh[20] = 75;  
					yygh[21] = 76;  
					yygh[22] = 76;  
					yygh[23] = 76;  
					yygh[24] = 76;  
					yygh[25] = 76;  
					yygh[26] = 76;  
					yygh[27] = 76;  
					yygh[28] = 76;  
					yygh[29] = 76;  
					yygh[30] = 76;  
					yygh[31] = 76;  
					yygh[32] = 76;  
					yygh[33] = 77;  
					yygh[34] = 77;  
					yygh[35] = 77;  
					yygh[36] = 77;  
					yygh[37] = 77;  
					yygh[38] = 77;  
					yygh[39] = 77;  
					yygh[40] = 77;  
					yygh[41] = 77;  
					yygh[42] = 77;  
					yygh[43] = 77;  
					yygh[44] = 77;  
					yygh[45] = 77;  
					yygh[46] = 77;  
					yygh[47] = 77;  
					yygh[48] = 77;  
					yygh[49] = 77;  
					yygh[50] = 78;  
					yygh[51] = 79;  
					yygh[52] = 79;  
					yygh[53] = 79;  
					yygh[54] = 79;  
					yygh[55] = 79;  
					yygh[56] = 79;  
					yygh[57] = 79;  
					yygh[58] = 79;  
					yygh[59] = 79;  
					yygh[60] = 79;  
					yygh[61] = 79;  
					yygh[62] = 79;  
					yygh[63] = 79;  
					yygh[64] = 79;  
					yygh[65] = 79;  
					yygh[66] = 79;  
					yygh[67] = 79;  
					yygh[68] = 79;  
					yygh[69] = 79;  
					yygh[70] = 80;  
					yygh[71] = 80;  
					yygh[72] = 80;  
					yygh[73] = 80;  
					yygh[74] = 80;  
					yygh[75] = 80;  
					yygh[76] = 80;  
					yygh[77] = 80;  
					yygh[78] = 80;  
					yygh[79] = 80;  
					yygh[80] = 80;  
					yygh[81] = 80;  
					yygh[82] = 81;  
					yygh[83] = 81;  
					yygh[84] = 82;  
					yygh[85] = 82;  
					yygh[86] = 82;  
					yygh[87] = 82;  
					yygh[88] = 88;  
					yygh[89] = 88;  
					yygh[90] = 104;  
					yygh[91] = 120;  
					yygh[92] = 120;  
					yygh[93] = 138;  
					yygh[94] = 138;  
					yygh[95] = 138;  
					yygh[96] = 155;  
					yygh[97] = 155;  
					yygh[98] = 162;  
					yygh[99] = 163;  
					yygh[100] = 163;  
					yygh[101] = 163;  
					yygh[102] = 163;  
					yygh[103] = 163;  
					yygh[104] = 163;  
					yygh[105] = 163;  
					yygh[106] = 163;  
					yygh[107] = 163;  
					yygh[108] = 163;  
					yygh[109] = 163;  
					yygh[110] = 163;  
					yygh[111] = 163;  
					yygh[112] = 163;  
					yygh[113] = 163;  
					yygh[114] = 163;  
					yygh[115] = 163;  
					yygh[116] = 163;  
					yygh[117] = 163;  
					yygh[118] = 163;  
					yygh[119] = 163;  
					yygh[120] = 164;  
					yygh[121] = 164;  
					yygh[122] = 181;  
					yygh[123] = 181;  
					yygh[124] = 181;  
					yygh[125] = 181;  
					yygh[126] = 182;  
					yygh[127] = 182;  
					yygh[128] = 199;  
					yygh[129] = 202;  
					yygh[130] = 205;  
					yygh[131] = 222;  
					yygh[132] = 246;  
					yygh[133] = 246;  
					yygh[134] = 270;  
					yygh[135] = 294;  
					yygh[136] = 294;  
					yygh[137] = 294;  
					yygh[138] = 294;  
					yygh[139] = 294;  
					yygh[140] = 295;  
					yygh[141] = 295;  
					yygh[142] = 295;  
					yygh[143] = 295;  
					yygh[144] = 295;  
					yygh[145] = 295;  
					yygh[146] = 302;  
					yygh[147] = 305;  
					yygh[148] = 308;  
					yygh[149] = 308;  
					yygh[150] = 308;  
					yygh[151] = 308;  
					yygh[152] = 308;  
					yygh[153] = 308;  
					yygh[154] = 308;  
					yygh[155] = 308;  
					yygh[156] = 311;  
					yygh[157] = 311;  
					yygh[158] = 311;  
					yygh[159] = 311;  
					yygh[160] = 311;  
					yygh[161] = 316;  
					yygh[162] = 321;  
					yygh[163] = 321;  
					yygh[164] = 340;  
					yygh[165] = 340;  
					yygh[166] = 357;  
					yygh[167] = 374;  
					yygh[168] = 398;  
					yygh[169] = 398;  
					yygh[170] = 398;  
					yygh[171] = 398;  
					yygh[172] = 398;  
					yygh[173] = 398;  
					yygh[174] = 398;  
					yygh[175] = 398;  
					yygh[176] = 398;  
					yygh[177] = 398;  
					yygh[178] = 398;  
					yygh[179] = 398;  
					yygh[180] = 398;  
					yygh[181] = 412;  
					yygh[182] = 412;  
					yygh[183] = 412;  
					yygh[184] = 413;  
					yygh[185] = 414;  
					yygh[186] = 415;  
					yygh[187] = 416;  
					yygh[188] = 416;  
					yygh[189] = 416;  
					yygh[190] = 416;  
					yygh[191] = 416;  
					yygh[192] = 416;  
					yygh[193] = 416;  
					yygh[194] = 417;  
					yygh[195] = 441;  
					yygh[196] = 441;  
					yygh[197] = 441;  
					yygh[198] = 458;  
					yygh[199] = 458;  
					yygh[200] = 458;  
					yygh[201] = 458;  
					yygh[202] = 475;  
					yygh[203] = 492;  
					yygh[204] = 499;  
					yygh[205] = 499;  
					yygh[206] = 499;  
					yygh[207] = 499;  
					yygh[208] = 499;  
					yygh[209] = 499;  
					yygh[210] = 499;  
					yygh[211] = 499;  
					yygh[212] = 514;  
					yygh[213] = 514;  
					yygh[214] = 514;  
					yygh[215] = 514;  
					yygh[216] = 514;  
					yygh[217] = 514;  
					yygh[218] = 514;  
					yygh[219] = 514;  
					yygh[220] = 514;  
					yygh[221] = 515;  
					yygh[222] = 515;  
					yygh[223] = 515;  
					yygh[224] = 515;  
					yygh[225] = 515;  
					yygh[226] = 515;  
					yygh[227] = 515;  
					yygh[228] = 515;  
					yygh[229] = 515;  
					yygh[230] = 515;  
					yygh[231] = 533;  
					yygh[232] = 551;  
					yygh[233] = 568;  
					yygh[234] = 592;  
					yygh[235] = 592;  
					yygh[236] = 606;  
					yygh[237] = 606;  
					yygh[238] = 606;  
					yygh[239] = 606;  
					yygh[240] = 621;  
					yygh[241] = 621;  
					yygh[242] = 621;  
					yygh[243] = 637;  
					yygh[244] = 637;  
					yygh[245] = 637;  
					yygh[246] = 637;  
					yygh[247] = 637;  
					yygh[248] = 654;  
					yygh[249] = 654;  
					yygh[250] = 654;  
					yygh[251] = 672;  
					yygh[252] = 691;  
					yygh[253] = 711;  
					yygh[254] = 732;  
					yygh[255] = 754;  
					yygh[256] = 778;  
					yygh[257] = 778;  
					yygh[258] = 778;  
					yygh[259] = 795;  
					yygh[260] = 812;  
					yygh[261] = 812;  
					yygh[262] = 812;  
					yygh[263] = 812;  
					yygh[264] = 813;  
					yygh[265] = 813;  
					yygh[266] = 813;  
					yygh[267] = 813;  
					yygh[268] = 813;  
					yygh[269] = 813;  
					yygh[270] = 813;  
					yygh[271] = 813;  
					yygh[272] = 813;  
					yygh[273] = 821;  
					yygh[274] = 829;  
					yygh[275] = 832;  
					yygh[276] = 832;  
					yygh[277] = 832;  
					yygh[278] = 851;  
					yygh[279] = 851;  
					yygh[280] = 851;  
					yygh[281] = 851;  
					yygh[282] = 851;  
					yygh[283] = 851;  
					yygh[284] = 851;  
					yygh[285] = 852;  
					yygh[286] = 853;  
					yygh[287] = 854;  
					yygh[288] = 855;  
					yygh[289] = 855;  
					yygh[290] = 855;  
					yygh[291] = 855;  
					yygh[292] = 855;  
					yygh[293] = 855;  
					yygh[294] = 855;  
					yygh[295] = 855;  
					yygh[296] = 855;  
					yygh[297] = 855;  
					yygh[298] = 860;  
					yygh[299] = 877;  
					yygh[300] = 882;  
					yygh[301] = 882;  
					yygh[302] = 882;  
					yygh[303] = 882;  
					yygh[304] = 882;  
					yygh[305] = 882;  
					yygh[306] = 882;  
					yygh[307] = 882;  
					yygh[308] = 882;  
					yygh[309] = 882;  
					yygh[310] = 882;  
					yygh[311] = 882;  
					yygh[312] = 882;  
					yygh[313] = 882;  
					yygh[314] = 882;  
					yygh[315] = 882;  
					yygh[316] = 882;  
					yygh[317] = 882;  
					yygh[318] = 882;  
					yygh[319] = 882;  
					yygh[320] = 882;  
					yygh[321] = 882;  
					yygh[322] = 882;  
					yygh[323] = 899;  
					yygh[324] = 906;  
					yygh[325] = 906;  
					yygh[326] = 906;  
					yygh[327] = 906;  
					yygh[328] = 906; 

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
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++;
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
		/// Scanner - Optimized
		////////////////////////////////////////////////////////////////

		public bool ScannerOpt (string Input)
		{
			if (Input.Length == 0) return true;
			TokenList = new ArrayList();
			while (1==1)
			{
				AToken lasttoken = FindTokenOpt(Input);
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
		public AToken FindTokenOpt (string Rest)
		{
			ArrayList Results  = new ArrayList();
			ArrayList ResultsV = new ArrayList();
			Match m;
			try{

				for (int idx = 0; idx < tList.Count; idx++)
				{
					m = rList[idx].Match(Rest);
					if (m.Success)
					{
						Results.Add(tList[idx]);
						ResultsV.Add(m.Value);
					}
				}

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
		public static string SubScanner(string file)
		{
			StreamReader in_s = File.OpenText(file);
			string inputstream = in_s.ReadToEnd();
			in_s.Close();

			MyCompiler compiler = new MyCompiler();
			StringBuilder sb = new StringBuilder();
			compiler.Output = new StringWriter(sb);
			if (!compiler.ScannerOpt(inputstream))
				return string.Empty;
			compiler.InitTables();
			if (!compiler.yyparse())
				return string.Empty;

			if (compiler.Output != null)
				compiler.Output.Close();
			return sb.ToString();
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

			if (Regex.IsMatch(Rest,"^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPECT|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPECT|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRICTION|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS_[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|NODE|PANEL_LAYER|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(CORR|FAC)|TOUCH_(DIST|MODE|STATE)|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRICTION|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS_[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|NODE|PANEL_LAYER|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(CORR|FAC)|TOUCH_(DIST|MODE|STATE)|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(SIN|ASIN|SQRT|ABS))")){
				Results.Add (t_ambigChar95mathChar95command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(SIN|ASIN|SQRT|ABS))").Value);}

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
