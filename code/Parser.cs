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
            rList.Add(new Regex("\\G((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("\\G((,*[\\s\\t\\x00]*)?;)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("\\G((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("\\G((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("\\G((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("\\G((?i)DEFINE)"));
            tList.Add(t_Char44);
            rList.Add(new Regex("\\G(:=|((,+[\\s\\t\\x00]*)*))"));
            tList.Add(t_UNDEF);
            rList.Add(new Regex("\\G((?i)UNDEF)"));
            tList.Add(t_INCLUDE);
            rList.Add(new Regex("\\G((?i)INCLUDE)"));
            tList.Add(t_Char123);
            rList.Add(new Regex("\\G(\\{)"));
            tList.Add(t_Char125);
            rList.Add(new Regex("\\G(\\})"));
            tList.Add(t_Char58);
            rList.Add(new Regex("\\G(:)"));
            tList.Add(t_Char124Char124);
            rList.Add(new Regex("\\G(\\|\\|)"));
            tList.Add(t_Char38Char38);
            rList.Add(new Regex("\\G(&&)"));
            tList.Add(t_Char124);
            rList.Add(new Regex("\\G(\\|)"));
            tList.Add(t_Char94);
            rList.Add(new Regex("\\G(\\^)"));
            tList.Add(t_Char38);
            rList.Add(new Regex("\\G(&)"));
            tList.Add(t_Char40);
            rList.Add(new Regex("\\G(\\()"));
            tList.Add(t_Char41);
            rList.Add(new Regex("\\G(\\))"));
            tList.Add(t_Char33Char61);
            rList.Add(new Regex("\\G(!=)"));
            tList.Add(t_Char61Char61);
            rList.Add(new Regex("\\G(==)"));
            tList.Add(t_Char60);
            rList.Add(new Regex("\\G(<)"));
            tList.Add(t_Char60Char61);
            rList.Add(new Regex("\\G(<=)"));
            tList.Add(t_Char62);
            rList.Add(new Regex("\\G(>)"));
            tList.Add(t_Char62Char61);
            rList.Add(new Regex("\\G(>=)"));
            tList.Add(t_Char43);
            rList.Add(new Regex("\\G(\\+)"));
            tList.Add(t_Char45);
            rList.Add(new Regex("\\G(\\-)"));
            tList.Add(t_Char37);
            rList.Add(new Regex("\\G(%)"));
            tList.Add(t_Char42);
            rList.Add(new Regex("\\G(\\*)"));
            tList.Add(t_Char47);
            rList.Add(new Regex("\\G(/)"));
            tList.Add(t_Char33);
            rList.Add(new Regex("\\G(!)"));
            tList.Add(t_RULE);
            rList.Add(new Regex("\\G((?i)RULE)"));
            tList.Add(t_Char42Char61);
            rList.Add(new Regex("\\G(\\*[\\s\\t\\x00]*=)"));
            tList.Add(t_Char43Char61);
            rList.Add(new Regex("\\G(\\+[\\s\\t\\x00]*=)"));
            tList.Add(t_Char45Char61);
            rList.Add(new Regex("\\G(\\-[\\s\\t\\x00]*=)"));
            tList.Add(t_Char47Char61);
            rList.Add(new Regex("\\G(/[\\s\\t\\x00]*=)"));
            tList.Add(t_Char61);
            rList.Add(new Regex("\\G(=)"));
            tList.Add(t_ELSE);
            rList.Add(new Regex("\\G((?i)ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("\\G((?i)IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("\\G((?i)WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("\\G(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("\\G((?i)NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("\\G((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("\\G((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))"));
            tList.Add(t_asset);
            rList.Add(new Regex("\\G((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("\\G((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("\\G((?i)(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("\\G((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_flag);
            rList.Add(new Regex("\\G((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("\\G((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("\\G((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("\\G((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("\\G((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("\\G((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("\\G((?i)CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("\\G((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("\\G((?i)(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("\\G((?i)(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("\\G((?i)RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("\\G((?i)HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("\\G((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))"));
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("\\G((?i)SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("\\G((?i)MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("\\G((?i)DO)"));
            tList.Add(t_ambigChar95skillChar95flag);
            rList.Add(new Regex("\\G((?i)(FLAG[1-8]))"));
            tList.Add(t_integer);
            rList.Add(new Regex("\\G([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("\\G(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("\\G([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("\\G(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("\\G(\"(\\\\\"|.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("\\G([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
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
        int yymaxdepth = 20000;
        int yyflag = 0;
        int yyfnone = 0;
        int[] yys = new int[20000];
        string[] yyv = new string[20000];

        string yyval = "";

        FileStream OutputStream;
        //StreamWriter Output;
        TextWriter Output;
        bool DeserializeOutput = false;
        string Scriptname = "Script";

        class YYARec
        {
            public int sym;
            public int act;
            public YYARec(int s, int a) { sym = s; act = a; }
        }

        class YYRRec
        {
            public int len;
            public int sym;
            public YYRRec(int l, int s) { sym = s; len = l; }
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
                int t_ambigChar95skillChar95flag = 321;
                int t_integer = 322;
                int t_fixed = 323;
                int t_identifier = 324;
                int t_file = 325;
                int t_string = 326;
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
            string Scriptname = "";

            foreach (string s in args)
            {
                if (s.ToLower() == "-t")
                {
                    ShowTokens = true;
                }
                else
                {
                    if (InputFilename == "") InputFilename = s;
                    else
                    if (OutputFilename == "") OutputFilename = s;
                    else
                    if (Scriptname == "") Scriptname = s;
                    else
                    {
                        Console.WriteLine("Too many arguments!");
                        return 1;
                    }
                }
            }
            if (InputFilename == "")
            {
                System.Console.WriteLine("You need to specify input and (optional) outputfile: compiler.exe input.txt [output.txt] [C# Class Identifier]");
                return 1;
            }
            if (Scriptname == "")
            {
                Scriptname = Path.GetFileNameWithoutExtension(InputFilename);
            }

            StreamReader in_s = File.OpenText(InputFilename);
            string inputstream = in_s.ReadToEnd();
            in_s.Close();

            ////////////////////////////////////////////////////////////////
            /// Compiler Code:
            ////////////////////////////////////////////////////////////////
            MyCompiler compiler = new MyCompiler();
            compiler.Output = null;
            compiler.DeserializeOutput = true;
            compiler.Scriptname = Scriptname;
            if (OutputFilename != "")
            {
                File.Delete(OutputFilename);
                compiler.OutputStream = File.OpenWrite(OutputFilename);
                compiler.Output = new StreamWriter(compiler.OutputStream, new System.Text.UTF8Encoding(false));
            }
            else
            {
                compiler.Output = new StreamWriter(Console.OpenStandardOutput(), new System.Text.UTF8Encoding(false));

            }

            //                        if (!compiler.Scanner(inputstream)) return 1;
            if (!compiler.ScannerOpt(inputstream)) return 1;
            if (ShowTokens)
            {
                foreach (AToken t in compiler.TokenList)
                {
                    Console.WriteLine("TokenID: " + t.token + "  =  " + t.val);
                }
            }
            compiler.InitTables();
            if (!compiler.yyparse()) return 1;

            if (compiler.Output != null) compiler.Output.Close();
            return 0;
        }
        public void yyaction(int yyruleno)
        {
            switch (yyruleno)
            {
                ////////////////////////////////////////////////////////////////
                /// YYAction code:
                ////////////////////////////////////////////////////////////////
							case    1 : 
         yyval = yyv[yysp-0];
         if (DeserializeOutput)
         Output.WriteLine(Script.Format(Scriptname, yyval));
         else
         Output.Write(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    3 : 
         yyval = "";
         
       break;
							case    4 : 
         yyval = "";
         
       break;
							case    5 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    6 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    7 : 
         yyval = yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   10 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   11 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   13 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   14 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   15 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   19 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   20 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   21 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   22 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   23 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   24 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   25 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   26 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   27 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   28 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   29 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   30 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddListDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   36 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-3]);
         
       break;
							case   37 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   38 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   43 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   44 : 
         //yyval = yyv[yysp-1];
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   45 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Globals.AddParameter(yyv[yysp-2]);
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3]);
         
       break;
							case   51 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-2]);
         
       break;
							case   52 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-0]);
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = yyv[yysp-0];
         
       break;
							case   55 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   56 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddStringObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   57 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
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
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = "";
         
       break;
							case   64 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   65 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   66 : 
         Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
       break;
							case   67 : 
         yyval = yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
       break;
							case   71 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   74 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   75 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   76 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   78 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = "";
         
       break;
							case   80 : 
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   81 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-3]);
         
       break;
							case   82 : 
         //Capture and discard bogus code
         yyval = Actions.CreateInvalidInstruction(yyv[yysp-3]);
         
       break;
							case   83 : 
         //yyval = yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   84 : 
         yyval = yyv[yysp-1];
         
       break;
							case   85 : 
         yyval = "";
         
       break;
							case   86 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   87 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
       break;
							case   88 : 
         yyval = yyv[yysp-0];
         
       break;
							case   89 : 
         yyval = yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  117 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  118 : 
         yyval = yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-0];
         
       break;
							case  120 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  121 : 
         yyval = " != ";
         
       break;
							case  122 : 
         yyval = " == ";
         
       break;
							case  123 : 
         yyval = " < ";
         
       break;
							case  124 : 
         yyval = " <= ";
         
       break;
							case  125 : 
         yyval = " > ";
         
       break;
							case  126 : 
         yyval = " >= ";
         
       break;
							case  127 : 
         yyval = " + ";
         
       break;
							case  128 : 
         yyval = " - ";
         
       break;
							case  129 : 
         yyval = " % ";
         
       break;
							case  130 : 
         yyval = " * ";
         
       break;
							case  131 : 
         yyval = " / ";
         
       break;
							case  132 : 
         yyval = "!";
         
       break;
							case  133 : 
         yyval = "+";
         
       break;
							case  134 : 
         yyval = "-";
         
       break;
							case  135 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  136 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  137 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  138 : 
         yyval = " *= ";
         
       break;
							case  139 : 
         yyval = " += ";
         
       break;
							case  140 : 
         yyval = " -= ";
         
       break;
							case  141 : 
         yyval = " /= ";
         
       break;
							case  142 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  147 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  148 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  149 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  150 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  155 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  156 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  157 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  158 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  173 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  174 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  175 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  176 : 
         yyval = yyv[yysp-0];
         
       break;
							case  177 : 
         yyval = yyv[yysp-0];
         
       break;
							case  178 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  179 : 
         yyval = yyv[yysp-0];
         
       break;
							case  180 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  183 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  184 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  185 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  186 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  187 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  191 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  196 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  197 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  198 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  204 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  205 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  206 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  207 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  208 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  209 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  210 : 
         yyval = yyv[yysp-0];
         
       break;
							case  211 : 
         yyval = yyv[yysp-0];
         
       break;
							case  212 : 
         yyval = yyv[yysp-0];
         
       break;
							case  213 : 
         yyval = yyv[yysp-0];
         
       break;
							case  214 : 
         yyval = yyv[yysp-0];
         
       break;
							case  215 : 
         yyval = yyv[yysp-0];
         
       break;
							case  216 : 
         yyval = yyv[yysp-0];
         
       break;
							case  217 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  218 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  219 : 
         yyval = Formatter.FormatString(yyv[yysp-0]);
         
       break;
               default: return;
            }
        }

        public void InitTables()
        {
            ////////////////////////////////////////////////////////////////
            /// Init Table code:
            ////////////////////////////////////////////////////////////////

					int yynacts   = 2499;
					int yyngotos  = 780;
					int yynstates = 341;
					int yynrules  = 219;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,70);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(258,85);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(258,104);yyac++; 
					yya[yyac] = new YYARec(258,105);yyac++; 
					yya[yyac] = new YYARec(263,106);yyac++; 
					yya[yyac] = new YYARec(258,107);yyac++; 
					yya[yyac] = new YYARec(258,108);yyac++; 
					yya[yyac] = new YYARec(266,109);yyac++; 
					yya[yyac] = new YYARec(266,111);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(322,113);yyac++; 
					yya[yyac] = new YYARec(323,114);yyac++; 
					yya[yyac] = new YYARec(258,115);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(258,117);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(258,155);yyac++; 
					yya[yyac] = new YYARec(257,163);yyac++; 
					yya[yyac] = new YYARec(259,164);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(258,-44 );yyac++; 
					yya[yyac] = new YYARec(258,174);yyac++; 
					yya[yyac] = new YYARec(260,175);yyac++; 
					yya[yyac] = new YYARec(261,176);yyac++; 
					yya[yyac] = new YYARec(258,177);yyac++; 
					yya[yyac] = new YYARec(297,178);yyac++; 
					yya[yyac] = new YYARec(258,-151 );yyac++; 
					yya[yyac] = new YYARec(263,-151 );yyac++; 
					yya[yyac] = new YYARec(266,-151 );yyac++; 
					yya[yyac] = new YYARec(269,-151 );yyac++; 
					yya[yyac] = new YYARec(270,-151 );yyac++; 
					yya[yyac] = new YYARec(271,-151 );yyac++; 
					yya[yyac] = new YYARec(272,-151 );yyac++; 
					yya[yyac] = new YYARec(273,-151 );yyac++; 
					yya[yyac] = new YYARec(275,-151 );yyac++; 
					yya[yyac] = new YYARec(276,-151 );yyac++; 
					yya[yyac] = new YYARec(277,-151 );yyac++; 
					yya[yyac] = new YYARec(278,-151 );yyac++; 
					yya[yyac] = new YYARec(279,-151 );yyac++; 
					yya[yyac] = new YYARec(280,-151 );yyac++; 
					yya[yyac] = new YYARec(281,-151 );yyac++; 
					yya[yyac] = new YYARec(282,-151 );yyac++; 
					yya[yyac] = new YYARec(283,-151 );yyac++; 
					yya[yyac] = new YYARec(284,-151 );yyac++; 
					yya[yyac] = new YYARec(285,-151 );yyac++; 
					yya[yyac] = new YYARec(286,-151 );yyac++; 
					yya[yyac] = new YYARec(287,-151 );yyac++; 
					yya[yyac] = new YYARec(289,-151 );yyac++; 
					yya[yyac] = new YYARec(290,-151 );yyac++; 
					yya[yyac] = new YYARec(291,-151 );yyac++; 
					yya[yyac] = new YYARec(292,-151 );yyac++; 
					yya[yyac] = new YYARec(293,-151 );yyac++; 
					yya[yyac] = new YYARec(298,-151 );yyac++; 
					yya[yyac] = new YYARec(299,-151 );yyac++; 
					yya[yyac] = new YYARec(300,-151 );yyac++; 
					yya[yyac] = new YYARec(301,-151 );yyac++; 
					yya[yyac] = new YYARec(302,-151 );yyac++; 
					yya[yyac] = new YYARec(303,-151 );yyac++; 
					yya[yyac] = new YYARec(304,-151 );yyac++; 
					yya[yyac] = new YYARec(305,-151 );yyac++; 
					yya[yyac] = new YYARec(306,-151 );yyac++; 
					yya[yyac] = new YYARec(307,-151 );yyac++; 
					yya[yyac] = new YYARec(308,-151 );yyac++; 
					yya[yyac] = new YYARec(309,-151 );yyac++; 
					yya[yyac] = new YYARec(310,-151 );yyac++; 
					yya[yyac] = new YYARec(311,-151 );yyac++; 
					yya[yyac] = new YYARec(312,-151 );yyac++; 
					yya[yyac] = new YYARec(313,-151 );yyac++; 
					yya[yyac] = new YYARec(314,-151 );yyac++; 
					yya[yyac] = new YYARec(315,-151 );yyac++; 
					yya[yyac] = new YYARec(316,-151 );yyac++; 
					yya[yyac] = new YYARec(317,-151 );yyac++; 
					yya[yyac] = new YYARec(318,-151 );yyac++; 
					yya[yyac] = new YYARec(319,-151 );yyac++; 
					yya[yyac] = new YYARec(320,-151 );yyac++; 
					yya[yyac] = new YYARec(321,-151 );yyac++; 
					yya[yyac] = new YYARec(322,-151 );yyac++; 
					yya[yyac] = new YYARec(323,-151 );yyac++; 
					yya[yyac] = new YYARec(324,-151 );yyac++; 
					yya[yyac] = new YYARec(325,-151 );yyac++; 
					yya[yyac] = new YYARec(326,-151 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,113);yyac++; 
					yya[yyac] = new YYARec(323,114);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(258,180);yyac++; 
					yya[yyac] = new YYARec(297,178);yyac++; 
					yya[yyac] = new YYARec(289,-151 );yyac++; 
					yya[yyac] = new YYARec(290,-151 );yyac++; 
					yya[yyac] = new YYARec(291,-151 );yyac++; 
					yya[yyac] = new YYARec(292,-151 );yyac++; 
					yya[yyac] = new YYARec(293,-151 );yyac++; 
					yya[yyac] = new YYARec(268,-217 );yyac++; 
					yya[yyac] = new YYARec(289,182);yyac++; 
					yya[yyac] = new YYARec(290,183);yyac++; 
					yya[yyac] = new YYARec(291,184);yyac++; 
					yya[yyac] = new YYARec(292,185);yyac++; 
					yya[yyac] = new YYARec(293,186);yyac++; 
					yya[yyac] = new YYARec(258,187);yyac++; 
					yya[yyac] = new YYARec(258,189);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(308,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(268,192);yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(308,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(268,-210 );yyac++; 
					yya[yyac] = new YYARec(289,-210 );yyac++; 
					yya[yyac] = new YYARec(290,-210 );yyac++; 
					yya[yyac] = new YYARec(291,-210 );yyac++; 
					yya[yyac] = new YYARec(292,-210 );yyac++; 
					yya[yyac] = new YYARec(293,-210 );yyac++; 
					yya[yyac] = new YYARec(297,-210 );yyac++; 
					yya[yyac] = new YYARec(267,194);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(266,223);yyac++; 
					yya[yyac] = new YYARec(258,-169 );yyac++; 
					yya[yyac] = new YYARec(263,-169 );yyac++; 
					yya[yyac] = new YYARec(282,-169 );yyac++; 
					yya[yyac] = new YYARec(283,-169 );yyac++; 
					yya[yyac] = new YYARec(287,-169 );yyac++; 
					yya[yyac] = new YYARec(298,-169 );yyac++; 
					yya[yyac] = new YYARec(299,-169 );yyac++; 
					yya[yyac] = new YYARec(300,-169 );yyac++; 
					yya[yyac] = new YYARec(301,-169 );yyac++; 
					yya[yyac] = new YYARec(302,-169 );yyac++; 
					yya[yyac] = new YYARec(303,-169 );yyac++; 
					yya[yyac] = new YYARec(304,-169 );yyac++; 
					yya[yyac] = new YYARec(305,-169 );yyac++; 
					yya[yyac] = new YYARec(306,-169 );yyac++; 
					yya[yyac] = new YYARec(307,-169 );yyac++; 
					yya[yyac] = new YYARec(308,-169 );yyac++; 
					yya[yyac] = new YYARec(309,-169 );yyac++; 
					yya[yyac] = new YYARec(310,-169 );yyac++; 
					yya[yyac] = new YYARec(311,-169 );yyac++; 
					yya[yyac] = new YYARec(312,-169 );yyac++; 
					yya[yyac] = new YYARec(313,-169 );yyac++; 
					yya[yyac] = new YYARec(314,-169 );yyac++; 
					yya[yyac] = new YYARec(315,-169 );yyac++; 
					yya[yyac] = new YYARec(316,-169 );yyac++; 
					yya[yyac] = new YYARec(317,-169 );yyac++; 
					yya[yyac] = new YYARec(318,-169 );yyac++; 
					yya[yyac] = new YYARec(319,-169 );yyac++; 
					yya[yyac] = new YYARec(320,-169 );yyac++; 
					yya[yyac] = new YYARec(321,-169 );yyac++; 
					yya[yyac] = new YYARec(322,-169 );yyac++; 
					yya[yyac] = new YYARec(323,-169 );yyac++; 
					yya[yyac] = new YYARec(324,-169 );yyac++; 
					yya[yyac] = new YYARec(325,-169 );yyac++; 
					yya[yyac] = new YYARec(326,-169 );yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
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
					yya[yyac] = new YYARec(303,-173 );yyac++; 
					yya[yyac] = new YYARec(304,-173 );yyac++; 
					yya[yyac] = new YYARec(305,-173 );yyac++; 
					yya[yyac] = new YYARec(306,-173 );yyac++; 
					yya[yyac] = new YYARec(307,-173 );yyac++; 
					yya[yyac] = new YYARec(308,-173 );yyac++; 
					yya[yyac] = new YYARec(309,-173 );yyac++; 
					yya[yyac] = new YYARec(310,-173 );yyac++; 
					yya[yyac] = new YYARec(311,-173 );yyac++; 
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
					yya[yyac] = new YYARec(326,-173 );yyac++; 
					yya[yyac] = new YYARec(268,-194 );yyac++; 
					yya[yyac] = new YYARec(289,-194 );yyac++; 
					yya[yyac] = new YYARec(290,-194 );yyac++; 
					yya[yyac] = new YYARec(291,-194 );yyac++; 
					yya[yyac] = new YYARec(292,-194 );yyac++; 
					yya[yyac] = new YYARec(293,-194 );yyac++; 
					yya[yyac] = new YYARec(297,-194 );yyac++; 
					yya[yyac] = new YYARec(258,-172 );yyac++; 
					yya[yyac] = new YYARec(263,-172 );yyac++; 
					yya[yyac] = new YYARec(282,-172 );yyac++; 
					yya[yyac] = new YYARec(283,-172 );yyac++; 
					yya[yyac] = new YYARec(287,-172 );yyac++; 
					yya[yyac] = new YYARec(298,-172 );yyac++; 
					yya[yyac] = new YYARec(299,-172 );yyac++; 
					yya[yyac] = new YYARec(300,-172 );yyac++; 
					yya[yyac] = new YYARec(301,-172 );yyac++; 
					yya[yyac] = new YYARec(302,-172 );yyac++; 
					yya[yyac] = new YYARec(303,-172 );yyac++; 
					yya[yyac] = new YYARec(304,-172 );yyac++; 
					yya[yyac] = new YYARec(305,-172 );yyac++; 
					yya[yyac] = new YYARec(306,-172 );yyac++; 
					yya[yyac] = new YYARec(307,-172 );yyac++; 
					yya[yyac] = new YYARec(308,-172 );yyac++; 
					yya[yyac] = new YYARec(309,-172 );yyac++; 
					yya[yyac] = new YYARec(310,-172 );yyac++; 
					yya[yyac] = new YYARec(311,-172 );yyac++; 
					yya[yyac] = new YYARec(312,-172 );yyac++; 
					yya[yyac] = new YYARec(313,-172 );yyac++; 
					yya[yyac] = new YYARec(314,-172 );yyac++; 
					yya[yyac] = new YYARec(315,-172 );yyac++; 
					yya[yyac] = new YYARec(316,-172 );yyac++; 
					yya[yyac] = new YYARec(317,-172 );yyac++; 
					yya[yyac] = new YYARec(318,-172 );yyac++; 
					yya[yyac] = new YYARec(319,-172 );yyac++; 
					yya[yyac] = new YYARec(320,-172 );yyac++; 
					yya[yyac] = new YYARec(321,-172 );yyac++; 
					yya[yyac] = new YYARec(322,-172 );yyac++; 
					yya[yyac] = new YYARec(323,-172 );yyac++; 
					yya[yyac] = new YYARec(324,-172 );yyac++; 
					yya[yyac] = new YYARec(325,-172 );yyac++; 
					yya[yyac] = new YYARec(326,-172 );yyac++; 
					yya[yyac] = new YYARec(268,-191 );yyac++; 
					yya[yyac] = new YYARec(289,-191 );yyac++; 
					yya[yyac] = new YYARec(290,-191 );yyac++; 
					yya[yyac] = new YYARec(291,-191 );yyac++; 
					yya[yyac] = new YYARec(292,-191 );yyac++; 
					yya[yyac] = new YYARec(293,-191 );yyac++; 
					yya[yyac] = new YYARec(297,-191 );yyac++; 
					yya[yyac] = new YYARec(258,-170 );yyac++; 
					yya[yyac] = new YYARec(263,-170 );yyac++; 
					yya[yyac] = new YYARec(282,-170 );yyac++; 
					yya[yyac] = new YYARec(283,-170 );yyac++; 
					yya[yyac] = new YYARec(287,-170 );yyac++; 
					yya[yyac] = new YYARec(298,-170 );yyac++; 
					yya[yyac] = new YYARec(299,-170 );yyac++; 
					yya[yyac] = new YYARec(300,-170 );yyac++; 
					yya[yyac] = new YYARec(301,-170 );yyac++; 
					yya[yyac] = new YYARec(302,-170 );yyac++; 
					yya[yyac] = new YYARec(303,-170 );yyac++; 
					yya[yyac] = new YYARec(304,-170 );yyac++; 
					yya[yyac] = new YYARec(305,-170 );yyac++; 
					yya[yyac] = new YYARec(306,-170 );yyac++; 
					yya[yyac] = new YYARec(307,-170 );yyac++; 
					yya[yyac] = new YYARec(308,-170 );yyac++; 
					yya[yyac] = new YYARec(309,-170 );yyac++; 
					yya[yyac] = new YYARec(310,-170 );yyac++; 
					yya[yyac] = new YYARec(311,-170 );yyac++; 
					yya[yyac] = new YYARec(312,-170 );yyac++; 
					yya[yyac] = new YYARec(313,-170 );yyac++; 
					yya[yyac] = new YYARec(314,-170 );yyac++; 
					yya[yyac] = new YYARec(315,-170 );yyac++; 
					yya[yyac] = new YYARec(316,-170 );yyac++; 
					yya[yyac] = new YYARec(317,-170 );yyac++; 
					yya[yyac] = new YYARec(318,-170 );yyac++; 
					yya[yyac] = new YYARec(319,-170 );yyac++; 
					yya[yyac] = new YYARec(320,-170 );yyac++; 
					yya[yyac] = new YYARec(321,-170 );yyac++; 
					yya[yyac] = new YYARec(322,-170 );yyac++; 
					yya[yyac] = new YYARec(323,-170 );yyac++; 
					yya[yyac] = new YYARec(324,-170 );yyac++; 
					yya[yyac] = new YYARec(325,-170 );yyac++; 
					yya[yyac] = new YYARec(326,-170 );yyac++; 
					yya[yyac] = new YYARec(268,-187 );yyac++; 
					yya[yyac] = new YYARec(289,-187 );yyac++; 
					yya[yyac] = new YYARec(290,-187 );yyac++; 
					yya[yyac] = new YYARec(291,-187 );yyac++; 
					yya[yyac] = new YYARec(292,-187 );yyac++; 
					yya[yyac] = new YYARec(293,-187 );yyac++; 
					yya[yyac] = new YYARec(297,-187 );yyac++; 
					yya[yyac] = new YYARec(258,-171 );yyac++; 
					yya[yyac] = new YYARec(263,-171 );yyac++; 
					yya[yyac] = new YYARec(282,-171 );yyac++; 
					yya[yyac] = new YYARec(283,-171 );yyac++; 
					yya[yyac] = new YYARec(287,-171 );yyac++; 
					yya[yyac] = new YYARec(298,-171 );yyac++; 
					yya[yyac] = new YYARec(299,-171 );yyac++; 
					yya[yyac] = new YYARec(300,-171 );yyac++; 
					yya[yyac] = new YYARec(301,-171 );yyac++; 
					yya[yyac] = new YYARec(302,-171 );yyac++; 
					yya[yyac] = new YYARec(303,-171 );yyac++; 
					yya[yyac] = new YYARec(304,-171 );yyac++; 
					yya[yyac] = new YYARec(305,-171 );yyac++; 
					yya[yyac] = new YYARec(306,-171 );yyac++; 
					yya[yyac] = new YYARec(307,-171 );yyac++; 
					yya[yyac] = new YYARec(308,-171 );yyac++; 
					yya[yyac] = new YYARec(309,-171 );yyac++; 
					yya[yyac] = new YYARec(310,-171 );yyac++; 
					yya[yyac] = new YYARec(311,-171 );yyac++; 
					yya[yyac] = new YYARec(312,-171 );yyac++; 
					yya[yyac] = new YYARec(313,-171 );yyac++; 
					yya[yyac] = new YYARec(314,-171 );yyac++; 
					yya[yyac] = new YYARec(315,-171 );yyac++; 
					yya[yyac] = new YYARec(316,-171 );yyac++; 
					yya[yyac] = new YYARec(317,-171 );yyac++; 
					yya[yyac] = new YYARec(318,-171 );yyac++; 
					yya[yyac] = new YYARec(319,-171 );yyac++; 
					yya[yyac] = new YYARec(320,-171 );yyac++; 
					yya[yyac] = new YYARec(321,-171 );yyac++; 
					yya[yyac] = new YYARec(322,-171 );yyac++; 
					yya[yyac] = new YYARec(323,-171 );yyac++; 
					yya[yyac] = new YYARec(324,-171 );yyac++; 
					yya[yyac] = new YYARec(325,-171 );yyac++; 
					yya[yyac] = new YYARec(326,-171 );yyac++; 
					yya[yyac] = new YYARec(268,-188 );yyac++; 
					yya[yyac] = new YYARec(289,-188 );yyac++; 
					yya[yyac] = new YYARec(290,-188 );yyac++; 
					yya[yyac] = new YYARec(291,-188 );yyac++; 
					yya[yyac] = new YYARec(292,-188 );yyac++; 
					yya[yyac] = new YYARec(293,-188 );yyac++; 
					yya[yyac] = new YYARec(297,-188 );yyac++; 
					yya[yyac] = new YYARec(258,227);yyac++; 
					yya[yyac] = new YYARec(263,-182 );yyac++; 
					yya[yyac] = new YYARec(268,-182 );yyac++; 
					yya[yyac] = new YYARec(282,-182 );yyac++; 
					yya[yyac] = new YYARec(283,-182 );yyac++; 
					yya[yyac] = new YYARec(287,-182 );yyac++; 
					yya[yyac] = new YYARec(289,-182 );yyac++; 
					yya[yyac] = new YYARec(290,-182 );yyac++; 
					yya[yyac] = new YYARec(291,-182 );yyac++; 
					yya[yyac] = new YYARec(292,-182 );yyac++; 
					yya[yyac] = new YYARec(293,-182 );yyac++; 
					yya[yyac] = new YYARec(297,-182 );yyac++; 
					yya[yyac] = new YYARec(298,-182 );yyac++; 
					yya[yyac] = new YYARec(299,-182 );yyac++; 
					yya[yyac] = new YYARec(300,-182 );yyac++; 
					yya[yyac] = new YYARec(301,-182 );yyac++; 
					yya[yyac] = new YYARec(302,-182 );yyac++; 
					yya[yyac] = new YYARec(303,-182 );yyac++; 
					yya[yyac] = new YYARec(304,-182 );yyac++; 
					yya[yyac] = new YYARec(305,-182 );yyac++; 
					yya[yyac] = new YYARec(306,-182 );yyac++; 
					yya[yyac] = new YYARec(307,-182 );yyac++; 
					yya[yyac] = new YYARec(308,-182 );yyac++; 
					yya[yyac] = new YYARec(309,-182 );yyac++; 
					yya[yyac] = new YYARec(310,-182 );yyac++; 
					yya[yyac] = new YYARec(311,-182 );yyac++; 
					yya[yyac] = new YYARec(312,-182 );yyac++; 
					yya[yyac] = new YYARec(313,-182 );yyac++; 
					yya[yyac] = new YYARec(314,-182 );yyac++; 
					yya[yyac] = new YYARec(315,-182 );yyac++; 
					yya[yyac] = new YYARec(316,-182 );yyac++; 
					yya[yyac] = new YYARec(317,-182 );yyac++; 
					yya[yyac] = new YYARec(318,-182 );yyac++; 
					yya[yyac] = new YYARec(319,-182 );yyac++; 
					yya[yyac] = new YYARec(320,-182 );yyac++; 
					yya[yyac] = new YYARec(321,-182 );yyac++; 
					yya[yyac] = new YYARec(322,-182 );yyac++; 
					yya[yyac] = new YYARec(323,-182 );yyac++; 
					yya[yyac] = new YYARec(324,-182 );yyac++; 
					yya[yyac] = new YYARec(325,-182 );yyac++; 
					yya[yyac] = new YYARec(326,-182 );yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(258,229);yyac++; 
					yya[yyac] = new YYARec(267,230);yyac++; 
					yya[yyac] = new YYARec(257,163);yyac++; 
					yya[yyac] = new YYARec(259,164);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(258,237);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(305,240);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(313,241);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(316,242);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(318,243);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(321,244);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(258,258);yyac++; 
					yya[yyac] = new YYARec(267,259);yyac++; 
					yya[yyac] = new YYARec(289,182);yyac++; 
					yya[yyac] = new YYARec(290,183);yyac++; 
					yya[yyac] = new YYARec(291,184);yyac++; 
					yya[yyac] = new YYARec(292,185);yyac++; 
					yya[yyac] = new YYARec(293,186);yyac++; 
					yya[yyac] = new YYARec(258,-120 );yyac++; 
					yya[yyac] = new YYARec(269,-120 );yyac++; 
					yya[yyac] = new YYARec(270,-120 );yyac++; 
					yya[yyac] = new YYARec(271,-120 );yyac++; 
					yya[yyac] = new YYARec(272,-120 );yyac++; 
					yya[yyac] = new YYARec(273,-120 );yyac++; 
					yya[yyac] = new YYARec(276,-120 );yyac++; 
					yya[yyac] = new YYARec(277,-120 );yyac++; 
					yya[yyac] = new YYARec(278,-120 );yyac++; 
					yya[yyac] = new YYARec(279,-120 );yyac++; 
					yya[yyac] = new YYARec(280,-120 );yyac++; 
					yya[yyac] = new YYARec(281,-120 );yyac++; 
					yya[yyac] = new YYARec(282,-120 );yyac++; 
					yya[yyac] = new YYARec(283,-120 );yyac++; 
					yya[yyac] = new YYARec(284,-120 );yyac++; 
					yya[yyac] = new YYARec(285,-120 );yyac++; 
					yya[yyac] = new YYARec(286,-120 );yyac++; 
					yya[yyac] = new YYARec(274,261);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(284,264);yyac++; 
					yya[yyac] = new YYARec(285,265);yyac++; 
					yya[yyac] = new YYARec(286,266);yyac++; 
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
					yya[yyac] = new YYARec(282,-110 );yyac++; 
					yya[yyac] = new YYARec(283,-110 );yyac++; 
					yya[yyac] = new YYARec(282,268);yyac++; 
					yya[yyac] = new YYARec(283,269);yyac++; 
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
					yya[yyac] = new YYARec(278,-108 );yyac++; 
					yya[yyac] = new YYARec(279,-108 );yyac++; 
					yya[yyac] = new YYARec(280,-108 );yyac++; 
					yya[yyac] = new YYARec(281,-108 );yyac++; 
					yya[yyac] = new YYARec(278,271);yyac++; 
					yya[yyac] = new YYARec(279,272);yyac++; 
					yya[yyac] = new YYARec(280,273);yyac++; 
					yya[yyac] = new YYARec(281,274);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(273,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(276,-106 );yyac++; 
					yya[yyac] = new YYARec(277,-106 );yyac++; 
					yya[yyac] = new YYARec(276,276);yyac++; 
					yya[yyac] = new YYARec(277,277);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(272,-105 );yyac++; 
					yya[yyac] = new YYARec(273,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(273,278);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(271,-103 );yyac++; 
					yya[yyac] = new YYARec(272,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(272,279);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(271,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(271,280);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(270,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(270,281);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(269,-97 );yyac++; 
					yya[yyac] = new YYARec(275,-97 );yyac++; 
					yya[yyac] = new YYARec(269,282);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(266,-95 );yyac++; 
					yya[yyac] = new YYARec(275,-95 );yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,-145 );yyac++; 
					yya[yyac] = new YYARec(258,-199 );yyac++; 
					yya[yyac] = new YYARec(266,-199 );yyac++; 
					yya[yyac] = new YYARec(269,-199 );yyac++; 
					yya[yyac] = new YYARec(270,-199 );yyac++; 
					yya[yyac] = new YYARec(271,-199 );yyac++; 
					yya[yyac] = new YYARec(272,-199 );yyac++; 
					yya[yyac] = new YYARec(273,-199 );yyac++; 
					yya[yyac] = new YYARec(275,-199 );yyac++; 
					yya[yyac] = new YYARec(276,-199 );yyac++; 
					yya[yyac] = new YYARec(277,-199 );yyac++; 
					yya[yyac] = new YYARec(278,-199 );yyac++; 
					yya[yyac] = new YYARec(279,-199 );yyac++; 
					yya[yyac] = new YYARec(280,-199 );yyac++; 
					yya[yyac] = new YYARec(281,-199 );yyac++; 
					yya[yyac] = new YYARec(282,-199 );yyac++; 
					yya[yyac] = new YYARec(283,-199 );yyac++; 
					yya[yyac] = new YYARec(284,-199 );yyac++; 
					yya[yyac] = new YYARec(285,-199 );yyac++; 
					yya[yyac] = new YYARec(286,-199 );yyac++; 
					yya[yyac] = new YYARec(289,-199 );yyac++; 
					yya[yyac] = new YYARec(290,-199 );yyac++; 
					yya[yyac] = new YYARec(291,-199 );yyac++; 
					yya[yyac] = new YYARec(292,-199 );yyac++; 
					yya[yyac] = new YYARec(293,-199 );yyac++; 
					yya[yyac] = new YYARec(297,-199 );yyac++; 
					yya[yyac] = new YYARec(274,-143 );yyac++; 
					yya[yyac] = new YYARec(258,-191 );yyac++; 
					yya[yyac] = new YYARec(266,-191 );yyac++; 
					yya[yyac] = new YYARec(269,-191 );yyac++; 
					yya[yyac] = new YYARec(270,-191 );yyac++; 
					yya[yyac] = new YYARec(271,-191 );yyac++; 
					yya[yyac] = new YYARec(272,-191 );yyac++; 
					yya[yyac] = new YYARec(273,-191 );yyac++; 
					yya[yyac] = new YYARec(275,-191 );yyac++; 
					yya[yyac] = new YYARec(276,-191 );yyac++; 
					yya[yyac] = new YYARec(277,-191 );yyac++; 
					yya[yyac] = new YYARec(278,-191 );yyac++; 
					yya[yyac] = new YYARec(279,-191 );yyac++; 
					yya[yyac] = new YYARec(280,-191 );yyac++; 
					yya[yyac] = new YYARec(281,-191 );yyac++; 
					yya[yyac] = new YYARec(282,-191 );yyac++; 
					yya[yyac] = new YYARec(283,-191 );yyac++; 
					yya[yyac] = new YYARec(284,-191 );yyac++; 
					yya[yyac] = new YYARec(285,-191 );yyac++; 
					yya[yyac] = new YYARec(286,-191 );yyac++; 
					yya[yyac] = new YYARec(289,-191 );yyac++; 
					yya[yyac] = new YYARec(290,-191 );yyac++; 
					yya[yyac] = new YYARec(291,-191 );yyac++; 
					yya[yyac] = new YYARec(292,-191 );yyac++; 
					yya[yyac] = new YYARec(293,-191 );yyac++; 
					yya[yyac] = new YYARec(297,-191 );yyac++; 
					yya[yyac] = new YYARec(274,-144 );yyac++; 
					yya[yyac] = new YYARec(258,-203 );yyac++; 
					yya[yyac] = new YYARec(266,-203 );yyac++; 
					yya[yyac] = new YYARec(269,-203 );yyac++; 
					yya[yyac] = new YYARec(270,-203 );yyac++; 
					yya[yyac] = new YYARec(271,-203 );yyac++; 
					yya[yyac] = new YYARec(272,-203 );yyac++; 
					yya[yyac] = new YYARec(273,-203 );yyac++; 
					yya[yyac] = new YYARec(275,-203 );yyac++; 
					yya[yyac] = new YYARec(276,-203 );yyac++; 
					yya[yyac] = new YYARec(277,-203 );yyac++; 
					yya[yyac] = new YYARec(278,-203 );yyac++; 
					yya[yyac] = new YYARec(279,-203 );yyac++; 
					yya[yyac] = new YYARec(280,-203 );yyac++; 
					yya[yyac] = new YYARec(281,-203 );yyac++; 
					yya[yyac] = new YYARec(282,-203 );yyac++; 
					yya[yyac] = new YYARec(283,-203 );yyac++; 
					yya[yyac] = new YYARec(284,-203 );yyac++; 
					yya[yyac] = new YYARec(285,-203 );yyac++; 
					yya[yyac] = new YYARec(286,-203 );yyac++; 
					yya[yyac] = new YYARec(289,-203 );yyac++; 
					yya[yyac] = new YYARec(290,-203 );yyac++; 
					yya[yyac] = new YYARec(291,-203 );yyac++; 
					yya[yyac] = new YYARec(292,-203 );yyac++; 
					yya[yyac] = new YYARec(293,-203 );yyac++; 
					yya[yyac] = new YYARec(297,-203 );yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(266,285);yyac++; 
					yya[yyac] = new YYARec(266,286);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(257,163);yyac++; 
					yya[yyac] = new YYARec(259,164);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(258,295);yyac++; 
					yya[yyac] = new YYARec(258,296);yyac++; 
					yya[yyac] = new YYARec(322,113);yyac++; 
					yya[yyac] = new YYARec(263,297);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(258,298);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(308,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(258,301);yyac++; 
					yya[yyac] = new YYARec(258,302);yyac++; 
					yya[yyac] = new YYARec(260,303);yyac++; 
					yya[yyac] = new YYARec(261,304);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(274,217);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,218);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,219);yyac++; 
					yya[yyac] = new YYARec(315,220);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,221);yyac++; 
					yya[yyac] = new YYARec(323,222);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(275,317);yyac++; 
					yya[yyac] = new YYARec(267,318);yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(263,72);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(326,-94 );yyac++; 
					yya[yyac] = new YYARec(257,163);yyac++; 
					yya[yyac] = new YYARec(259,164);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(257,163);yyac++; 
					yya[yyac] = new YYARec(259,164);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(261,326);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,130);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(257,141);yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(259,143);yyac++; 
					yya[yyac] = new YYARec(264,144);yyac++; 
					yya[yyac] = new YYARec(266,145);yyac++; 
					yya[yyac] = new YYARec(288,146);yyac++; 
					yya[yyac] = new YYARec(294,147);yyac++; 
					yya[yyac] = new YYARec(295,148);yyac++; 
					yya[yyac] = new YYARec(296,149);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,150);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,151);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,152);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,153);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(324,154);yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(275,329);yyac++; 
					yya[yyac] = new YYARec(284,264);yyac++; 
					yya[yyac] = new YYARec(285,265);yyac++; 
					yya[yyac] = new YYARec(286,266);yyac++; 
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
					yya[yyac] = new YYARec(282,-111 );yyac++; 
					yya[yyac] = new YYARec(283,-111 );yyac++; 
					yya[yyac] = new YYARec(282,268);yyac++; 
					yya[yyac] = new YYARec(283,269);yyac++; 
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
					yya[yyac] = new YYARec(278,-109 );yyac++; 
					yya[yyac] = new YYARec(279,-109 );yyac++; 
					yya[yyac] = new YYARec(280,-109 );yyac++; 
					yya[yyac] = new YYARec(281,-109 );yyac++; 
					yya[yyac] = new YYARec(278,271);yyac++; 
					yya[yyac] = new YYARec(279,272);yyac++; 
					yya[yyac] = new YYARec(280,273);yyac++; 
					yya[yyac] = new YYARec(281,274);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(273,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(276,-107 );yyac++; 
					yya[yyac] = new YYARec(277,-107 );yyac++; 
					yya[yyac] = new YYARec(276,276);yyac++; 
					yya[yyac] = new YYARec(277,277);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(272,-104 );yyac++; 
					yya[yyac] = new YYARec(273,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(273,278);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(271,-102 );yyac++; 
					yya[yyac] = new YYARec(272,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(272,279);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(270,-100 );yyac++; 
					yya[yyac] = new YYARec(271,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(271,280);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(269,-98 );yyac++; 
					yya[yyac] = new YYARec(270,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(270,281);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(266,-96 );yyac++; 
					yya[yyac] = new YYARec(269,-96 );yyac++; 
					yya[yyac] = new YYARec(275,-96 );yyac++; 
					yya[yyac] = new YYARec(267,330);yyac++; 
					yya[yyac] = new YYARec(267,331);yyac++; 
					yya[yyac] = new YYARec(282,95);yyac++; 
					yya[yyac] = new YYARec(283,96);yyac++; 
					yya[yyac] = new YYARec(287,97);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,64);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,99);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(325,82);yyac++; 
					yya[yyac] = new YYARec(326,100);yyac++; 
					yya[yyac] = new YYARec(258,-65 );yyac++; 
					yya[yyac] = new YYARec(260,333);yyac++; 
					yya[yyac] = new YYARec(261,334);yyac++; 
					yya[yyac] = new YYARec(258,335);yyac++; 
					yya[yyac] = new YYARec(258,336);yyac++; 
					yya[yyac] = new YYARec(261,337);yyac++; 
					yya[yyac] = new YYARec(258,338);yyac++; 
					yya[yyac] = new YYARec(257,163);yyac++; 
					yya[yyac] = new YYARec(259,164);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,165);yyac++; 
					yya[yyac] = new YYARec(311,166);yyac++; 
					yya[yyac] = new YYARec(312,167);yyac++; 
					yya[yyac] = new YYARec(315,168);yyac++; 
					yya[yyac] = new YYARec(317,169);yyac++; 
					yya[yyac] = new YYARec(319,170);yyac++; 
					yya[yyac] = new YYARec(320,171);yyac++; 
					yya[yyac] = new YYARec(324,65);yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(261,340);yyac++;

					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,16);yygc++; 
					yyg[yygc] = new YYARec(-2,17);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,39);yygc++; 
					yyg[yygc] = new YYARec(-45,40);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,66);yygc++; 
					yyg[yygc] = new YYARec(-33,67);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,68);yygc++; 
					yyg[yygc] = new YYARec(-35,69);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-29,71);yygc++; 
					yyg[yygc] = new YYARec(-29,73);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,74);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,77);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,78);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,79);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,80);yygc++; 
					yyg[yygc] = new YYARec(-25,81);yygc++; 
					yyg[yygc] = new YYARec(-29,83);yygc++; 
					yyg[yygc] = new YYARec(-29,84);yygc++; 
					yyg[yygc] = new YYARec(-29,86);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-69,88);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-32,90);yygc++; 
					yyg[yygc] = new YYARec(-26,91);yygc++; 
					yyg[yygc] = new YYARec(-25,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-22,94);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,66);yygc++; 
					yyg[yygc] = new YYARec(-33,101);yygc++; 
					yyg[yygc] = new YYARec(-30,102);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-23,110);yygc++; 
					yyg[yygc] = new YYARec(-25,112);yygc++; 
					yyg[yygc] = new YYARec(-29,116);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,118);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,119);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,120);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,119);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,122);yygc++; 
					yyg[yygc] = new YYARec(-69,123);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-27,124);yygc++; 
					yyg[yygc] = new YYARec(-26,125);yygc++; 
					yyg[yygc] = new YYARec(-25,126);yygc++; 
					yyg[yygc] = new YYARec(-24,127);yygc++; 
					yyg[yygc] = new YYARec(-23,128);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-20,129);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,139);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-41,156);yygc++; 
					yyg[yygc] = new YYARec(-40,157);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-19,161);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++; 
					yyg[yygc] = new YYARec(-29,172);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,66);yygc++; 
					yyg[yygc] = new YYARec(-33,101);yygc++; 
					yyg[yygc] = new YYARec(-30,173);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,179);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-73,181);yygc++; 
					yyg[yygc] = new YYARec(-29,188);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,190);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,191);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-29,193);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,195);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,196);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,197);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,198);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,199);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,200);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,215);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,225);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,226);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-29,228);yygc++; 
					yyg[yygc] = new YYARec(-41,156);yygc++; 
					yyg[yygc] = new YYARec(-40,157);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-19,231);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,232);yygc++; 
					yyg[yygc] = new YYARec(-22,75);yygc++; 
					yyg[yygc] = new YYARec(-21,76);yygc++; 
					yyg[yygc] = new YYARec(-12,233);yygc++; 
					yyg[yygc] = new YYARec(-69,234);yygc++; 
					yyg[yygc] = new YYARec(-37,235);yygc++; 
					yyg[yygc] = new YYARec(-36,236);yygc++; 
					yyg[yygc] = new YYARec(-75,238);yygc++; 
					yyg[yygc] = new YYARec(-41,239);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,245);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,122);yygc++; 
					yyg[yygc] = new YYARec(-69,123);yygc++; 
					yyg[yygc] = new YYARec(-52,246);yygc++; 
					yyg[yygc] = new YYARec(-50,247);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-27,248);yygc++; 
					yyg[yygc] = new YYARec(-26,249);yygc++; 
					yyg[yygc] = new YYARec(-25,250);yygc++; 
					yyg[yygc] = new YYARec(-24,251);yygc++; 
					yyg[yygc] = new YYARec(-23,252);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,253);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,122);yygc++; 
					yyg[yygc] = new YYARec(-69,123);yygc++; 
					yyg[yygc] = new YYARec(-52,246);yygc++; 
					yyg[yygc] = new YYARec(-50,254);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-27,248);yygc++; 
					yyg[yygc] = new YYARec(-26,249);yygc++; 
					yyg[yygc] = new YYARec(-25,250);yygc++; 
					yyg[yygc] = new YYARec(-24,251);yygc++; 
					yyg[yygc] = new YYARec(-23,252);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,255);yygc++; 
					yyg[yygc] = new YYARec(-15,256);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,255);yygc++; 
					yyg[yygc] = new YYARec(-15,257);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-73,260);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,262);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-67,263);yygc++; 
					yyg[yygc] = new YYARec(-65,267);yygc++; 
					yyg[yygc] = new YYARec(-63,270);yygc++; 
					yyg[yygc] = new YYARec(-61,275);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,283);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,284);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,287);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,122);yygc++; 
					yyg[yygc] = new YYARec(-69,123);yygc++; 
					yyg[yygc] = new YYARec(-43,288);yygc++; 
					yyg[yygc] = new YYARec(-42,289);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-27,290);yygc++; 
					yyg[yygc] = new YYARec(-26,291);yygc++; 
					yyg[yygc] = new YYARec(-25,292);yygc++; 
					yyg[yygc] = new YYARec(-23,293);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-41,156);yygc++; 
					yyg[yygc] = new YYARec(-40,157);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-19,294);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,299);yygc++; 
					yyg[yygc] = new YYARec(-29,300);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,305);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,306);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,213);yygc++; 
					yyg[yygc] = new YYARec(-54,214);yygc++; 
					yyg[yygc] = new YYARec(-53,307);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,308);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,309);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,310);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,311);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,312);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,313);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,314);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,315);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,224);yygc++; 
					yyg[yygc] = new YYARec(-71,201);yygc++; 
					yyg[yygc] = new YYARec(-70,202);yygc++; 
					yyg[yygc] = new YYARec(-69,203);yygc++; 
					yyg[yygc] = new YYARec(-68,204);yygc++; 
					yyg[yygc] = new YYARec(-66,205);yygc++; 
					yyg[yygc] = new YYARec(-64,206);yygc++; 
					yyg[yygc] = new YYARec(-62,207);yygc++; 
					yyg[yygc] = new YYARec(-60,208);yygc++; 
					yyg[yygc] = new YYARec(-59,209);yygc++; 
					yyg[yygc] = new YYARec(-58,210);yygc++; 
					yyg[yygc] = new YYARec(-57,211);yygc++; 
					yyg[yygc] = new YYARec(-56,212);yygc++; 
					yyg[yygc] = new YYARec(-55,316);yygc++; 
					yyg[yygc] = new YYARec(-24,216);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,319);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,320);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-29,321);yygc++; 
					yyg[yygc] = new YYARec(-41,156);yygc++; 
					yyg[yygc] = new YYARec(-40,157);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-19,322);yygc++; 
					yyg[yygc] = new YYARec(-18,323);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++; 
					yyg[yygc] = new YYARec(-41,156);yygc++; 
					yyg[yygc] = new YYARec(-40,157);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-19,322);yygc++; 
					yyg[yygc] = new YYARec(-18,324);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++; 
					yyg[yygc] = new YYARec(-69,234);yygc++; 
					yyg[yygc] = new YYARec(-37,235);yygc++; 
					yyg[yygc] = new YYARec(-36,325);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,122);yygc++; 
					yyg[yygc] = new YYARec(-69,123);yygc++; 
					yyg[yygc] = new YYARec(-52,246);yygc++; 
					yyg[yygc] = new YYARec(-50,327);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-27,248);yygc++; 
					yyg[yygc] = new YYARec(-26,249);yygc++; 
					yyg[yygc] = new YYARec(-25,250);yygc++; 
					yyg[yygc] = new YYARec(-24,251);yygc++; 
					yyg[yygc] = new YYARec(-23,252);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-74,131);yygc++; 
					yyg[yygc] = new YYARec(-72,132);yygc++; 
					yyg[yygc] = new YYARec(-51,133);yygc++; 
					yyg[yygc] = new YYARec(-49,134);yygc++; 
					yyg[yygc] = new YYARec(-48,135);yygc++; 
					yyg[yygc] = new YYARec(-47,136);yygc++; 
					yyg[yygc] = new YYARec(-46,137);yygc++; 
					yyg[yygc] = new YYARec(-22,138);yygc++; 
					yyg[yygc] = new YYARec(-16,328);yygc++; 
					yyg[yygc] = new YYARec(-14,140);yygc++; 
					yyg[yygc] = new YYARec(-67,263);yygc++; 
					yyg[yygc] = new YYARec(-65,267);yygc++; 
					yyg[yygc] = new YYARec(-63,270);yygc++; 
					yyg[yygc] = new YYARec(-61,275);yygc++; 
					yyg[yygc] = new YYARec(-78,37);yygc++; 
					yyg[yygc] = new YYARec(-77,38);yygc++; 
					yyg[yygc] = new YYARec(-76,87);yygc++; 
					yyg[yygc] = new YYARec(-74,121);yygc++; 
					yyg[yygc] = new YYARec(-72,122);yygc++; 
					yyg[yygc] = new YYARec(-69,123);yygc++; 
					yyg[yygc] = new YYARec(-43,288);yygc++; 
					yyg[yygc] = new YYARec(-42,332);yygc++; 
					yyg[yygc] = new YYARec(-37,89);yygc++; 
					yyg[yygc] = new YYARec(-27,290);yygc++; 
					yyg[yygc] = new YYARec(-26,291);yygc++; 
					yyg[yygc] = new YYARec(-25,292);yygc++; 
					yyg[yygc] = new YYARec(-23,293);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-41,156);yygc++; 
					yyg[yygc] = new YYARec(-40,157);yygc++; 
					yyg[yygc] = new YYARec(-39,158);yygc++; 
					yyg[yygc] = new YYARec(-34,159);yygc++; 
					yyg[yygc] = new YYARec(-22,160);yygc++; 
					yyg[yygc] = new YYARec(-19,339);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -60;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = 0;  
					yyd[7] = -41;  
					yyd[8] = -12;  
					yyd[9] = -11;  
					yyd[10] = -10;  
					yyd[11] = -9;  
					yyd[12] = -8;  
					yyd[13] = -7;  
					yyd[14] = -6;  
					yyd[15] = 0;  
					yyd[16] = -1;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = -4;  
					yyd[20] = 0;  
					yyd[21] = 0;  
					yyd[22] = 0;  
					yyd[23] = 0;  
					yyd[24] = -5;  
					yyd[25] = -43;  
					yyd[26] = -40;  
					yyd[27] = -54;  
					yyd[28] = -58;  
					yyd[29] = -72;  
					yyd[30] = -186;  
					yyd[31] = -38;  
					yyd[32] = -42;  
					yyd[33] = -59;  
					yyd[34] = -184;  
					yyd[35] = -185;  
					yyd[36] = -39;  
					yyd[37] = -212;  
					yyd[38] = -211;  
					yyd[39] = -214;  
					yyd[40] = 0;  
					yyd[41] = -210;  
					yyd[42] = -209;  
					yyd[43] = -195;  
					yyd[44] = -198;  
					yyd[45] = -193;  
					yyd[46] = -200;  
					yyd[47] = -197;  
					yyd[48] = -199;  
					yyd[49] = -196;  
					yyd[50] = -201;  
					yyd[51] = -194;  
					yyd[52] = -207;  
					yyd[53] = -208;  
					yyd[54] = -190;  
					yyd[55] = -189;  
					yyd[56] = -192;  
					yyd[57] = -191;  
					yyd[58] = -203;  
					yyd[59] = -206;  
					yyd[60] = -205;  
					yyd[61] = -187;  
					yyd[62] = -202;  
					yyd[63] = -188;  
					yyd[64] = -204;  
					yyd[65] = -182;  
					yyd[66] = -213;  
					yyd[67] = 0;  
					yyd[68] = -216;  
					yyd[69] = 0;  
					yyd[70] = -215;  
					yyd[71] = 0;  
					yyd[72] = -93;  
					yyd[73] = 0;  
					yyd[74] = -2;  
					yyd[75] = -29;  
					yyd[76] = -28;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = 0;  
					yyd[82] = -218;  
					yyd[83] = 0;  
					yyd[84] = 0;  
					yyd[85] = -57;  
					yyd[86] = 0;  
					yyd[87] = -174;  
					yyd[88] = 0;  
					yyd[89] = -175;  
					yyd[90] = 0;  
					yyd[91] = -48;  
					yyd[92] = -49;  
					yyd[93] = -46;  
					yyd[94] = -47;  
					yyd[95] = -133;  
					yyd[96] = -134;  
					yyd[97] = -132;  
					yyd[98] = -179;  
					yyd[99] = -181;  
					yyd[100] = -219;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = -26;  
					yyd[106] = 0;  
					yyd[107] = -27;  
					yyd[108] = -35;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = -178;  
					yyd[114] = -180;  
					yyd[115] = -37;  
					yyd[116] = 0;  
					yyd[117] = -36;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = -152;  
					yyd[123] = 0;  
					yyd[124] = -34;  
					yyd[125] = -33;  
					yyd[126] = -32;  
					yyd[127] = -31;  
					yyd[128] = -30;  
					yyd[129] = 0;  
					yyd[130] = -183;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = 0;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = -85;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = 0;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = -56;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = -167;  
					yyd[159] = -168;  
					yyd[160] = -166;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = -165;  
					yyd[166] = -161;  
					yyd[167] = -160;  
					yyd[168] = -163;  
					yyd[169] = -164;  
					yyd[170] = -162;  
					yyd[171] = -159;  
					yyd[172] = 0;  
					yyd[173] = -45;  
					yyd[174] = -13;  
					yyd[175] = 0;  
					yyd[176] = -16;  
					yyd[177] = -14;  
					yyd[178] = 0;  
					yyd[179] = -153;  
					yyd[180] = -25;  
					yyd[181] = 0;  
					yyd[182] = -138;  
					yyd[183] = -139;  
					yyd[184] = -140;  
					yyd[185] = -141;  
					yyd[186] = -142;  
					yyd[187] = -84;  
					yyd[188] = 0;  
					yyd[189] = -83;  
					yyd[190] = -78;  
					yyd[191] = -76;  
					yyd[192] = 0;  
					yyd[193] = 0;  
					yyd[194] = -71;  
					yyd[195] = -77;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = 0;  
					yyd[201] = -119;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = -114;  
					yyd[205] = -112;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = 0;  
					yyd[209] = 0;  
					yyd[210] = 0;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = 0;  
					yyd[214] = 0;  
					yyd[215] = -135;  
					yyd[216] = -118;  
					yyd[217] = 0;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = -177;  
					yyd[222] = -176;  
					yyd[223] = 0;  
					yyd[224] = -120;  
					yyd[225] = 0;  
					yyd[226] = 0;  
					yyd[227] = 0;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = -55;  
					yyd[231] = -62;  
					yyd[232] = 0;  
					yyd[233] = 0;  
					yyd[234] = 0;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = 0;  
					yyd[238] = -149;  
					yyd[239] = -150;  
					yyd[240] = -158;  
					yyd[241] = -155;  
					yyd[242] = -157;  
					yyd[243] = -154;  
					yyd[244] = -156;  
					yyd[245] = -137;  
					yyd[246] = 0;  
					yyd[247] = 0;  
					yyd[248] = -88;  
					yyd[249] = -91;  
					yyd[250] = -92;  
					yyd[251] = -89;  
					yyd[252] = -90;  
					yyd[253] = -75;  
					yyd[254] = 0;  
					yyd[255] = 0;  
					yyd[256] = -17;  
					yyd[257] = -18;  
					yyd[258] = -80;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = 0;  
					yyd[262] = -115;  
					yyd[263] = 0;  
					yyd[264] = -129;  
					yyd[265] = -130;  
					yyd[266] = -131;  
					yyd[267] = 0;  
					yyd[268] = -127;  
					yyd[269] = -128;  
					yyd[270] = 0;  
					yyd[271] = -123;  
					yyd[272] = -124;  
					yyd[273] = -125;  
					yyd[274] = -126;  
					yyd[275] = 0;  
					yyd[276] = -121;  
					yyd[277] = -122;  
					yyd[278] = 0;  
					yyd[279] = 0;  
					yyd[280] = 0;  
					yyd[281] = 0;  
					yyd[282] = 0;  
					yyd[283] = 0;  
					yyd[284] = 0;  
					yyd[285] = 0;  
					yyd[286] = 0;  
					yyd[287] = -73;  
					yyd[288] = 0;  
					yyd[289] = -64;  
					yyd[290] = -68;  
					yyd[291] = -69;  
					yyd[292] = -70;  
					yyd[293] = -67;  
					yyd[294] = -61;  
					yyd[295] = 0;  
					yyd[296] = 0;  
					yyd[297] = 0;  
					yyd[298] = -50;  
					yyd[299] = 0;  
					yyd[300] = 0;  
					yyd[301] = -81;  
					yyd[302] = -82;  
					yyd[303] = 0;  
					yyd[304] = -20;  
					yyd[305] = -74;  
					yyd[306] = -136;  
					yyd[307] = 0;  
					yyd[308] = -113;  
					yyd[309] = 0;  
					yyd[310] = 0;  
					yyd[311] = 0;  
					yyd[312] = 0;  
					yyd[313] = 0;  
					yyd[314] = 0;  
					yyd[315] = 0;  
					yyd[316] = 0;  
					yyd[317] = -117;  
					yyd[318] = -146;  
					yyd[319] = 0;  
					yyd[320] = 0;  
					yyd[321] = 0;  
					yyd[322] = 0;  
					yyd[323] = 0;  
					yyd[324] = 0;  
					yyd[325] = -51;  
					yyd[326] = -15;  
					yyd[327] = -87;  
					yyd[328] = 0;  
					yyd[329] = -116;  
					yyd[330] = -147;  
					yyd[331] = -148;  
					yyd[332] = -66;  
					yyd[333] = 0;  
					yyd[334] = -24;  
					yyd[335] = -21;  
					yyd[336] = -22;  
					yyd[337] = -19;  
					yyd[338] = 0;  
					yyd[339] = 0;  
					yyd[340] = -23; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 21;  
					yyal[2] = 45;  
					yyal[3] = 45;  
					yyal[4] = 69;  
					yyal[5] = 94;  
					yyal[6] = 103;  
					yyal[7] = 128;  
					yyal[8] = 128;  
					yyal[9] = 128;  
					yyal[10] = 128;  
					yyal[11] = 128;  
					yyal[12] = 128;  
					yyal[13] = 128;  
					yyal[14] = 128;  
					yyal[15] = 128;  
					yyal[16] = 150;  
					yyal[17] = 150;  
					yyal[18] = 151;  
					yyal[19] = 155;  
					yyal[20] = 155;  
					yyal[21] = 159;  
					yyal[22] = 163;  
					yyal[23] = 167;  
					yyal[24] = 168;  
					yyal[25] = 168;  
					yyal[26] = 168;  
					yyal[27] = 168;  
					yyal[28] = 168;  
					yyal[29] = 168;  
					yyal[30] = 168;  
					yyal[31] = 168;  
					yyal[32] = 168;  
					yyal[33] = 168;  
					yyal[34] = 168;  
					yyal[35] = 168;  
					yyal[36] = 168;  
					yyal[37] = 168;  
					yyal[38] = 168;  
					yyal[39] = 168;  
					yyal[40] = 168;  
					yyal[41] = 170;  
					yyal[42] = 170;  
					yyal[43] = 170;  
					yyal[44] = 170;  
					yyal[45] = 170;  
					yyal[46] = 170;  
					yyal[47] = 170;  
					yyal[48] = 170;  
					yyal[49] = 170;  
					yyal[50] = 170;  
					yyal[51] = 170;  
					yyal[52] = 170;  
					yyal[53] = 170;  
					yyal[54] = 170;  
					yyal[55] = 170;  
					yyal[56] = 170;  
					yyal[57] = 170;  
					yyal[58] = 170;  
					yyal[59] = 170;  
					yyal[60] = 170;  
					yyal[61] = 170;  
					yyal[62] = 170;  
					yyal[63] = 170;  
					yyal[64] = 170;  
					yyal[65] = 170;  
					yyal[66] = 170;  
					yyal[67] = 170;  
					yyal[68] = 174;  
					yyal[69] = 174;  
					yyal[70] = 176;  
					yyal[71] = 176;  
					yyal[72] = 184;  
					yyal[73] = 184;  
					yyal[74] = 208;  
					yyal[75] = 208;  
					yyal[76] = 208;  
					yyal[77] = 208;  
					yyal[78] = 209;  
					yyal[79] = 210;  
					yyal[80] = 212;  
					yyal[81] = 213;  
					yyal[82] = 214;  
					yyal[83] = 214;  
					yyal[84] = 215;  
					yyal[85] = 217;  
					yyal[86] = 217;  
					yyal[87] = 218;  
					yyal[88] = 218;  
					yyal[89] = 220;  
					yyal[90] = 220;  
					yyal[91] = 221;  
					yyal[92] = 221;  
					yyal[93] = 221;  
					yyal[94] = 221;  
					yyal[95] = 221;  
					yyal[96] = 221;  
					yyal[97] = 221;  
					yyal[98] = 221;  
					yyal[99] = 221;  
					yyal[100] = 221;  
					yyal[101] = 221;  
					yyal[102] = 247;  
					yyal[103] = 248;  
					yyal[104] = 269;  
					yyal[105] = 290;  
					yyal[106] = 290;  
					yyal[107] = 322;  
					yyal[108] = 322;  
					yyal[109] = 322;  
					yyal[110] = 356;  
					yyal[111] = 357;  
					yyal[112] = 370;  
					yyal[113] = 376;  
					yyal[114] = 376;  
					yyal[115] = 376;  
					yyal[116] = 376;  
					yyal[117] = 401;  
					yyal[118] = 401;  
					yyal[119] = 402;  
					yyal[120] = 404;  
					yyal[121] = 405;  
					yyal[122] = 461;  
					yyal[123] = 461;  
					yyal[124] = 487;  
					yyal[125] = 487;  
					yyal[126] = 487;  
					yyal[127] = 487;  
					yyal[128] = 487;  
					yyal[129] = 487;  
					yyal[130] = 488;  
					yyal[131] = 488;  
					yyal[132] = 495;  
					yyal[133] = 500;  
					yyal[134] = 501;  
					yyal[135] = 535;  
					yyal[136] = 571;  
					yyal[137] = 607;  
					yyal[138] = 608;  
					yyal[139] = 648;  
					yyal[140] = 649;  
					yyal[141] = 685;  
					yyal[142] = 689;  
					yyal[143] = 689;  
					yyal[144] = 693;  
					yyal[145] = 697;  
					yyal[146] = 731;  
					yyal[147] = 762;  
					yyal[148] = 797;  
					yyal[149] = 828;  
					yyal[150] = 859;  
					yyal[151] = 900;  
					yyal[152] = 941;  
					yyal[153] = 982;  
					yyal[154] = 1023;  
					yyal[155] = 1064;  
					yyal[156] = 1064;  
					yyal[157] = 1096;  
					yyal[158] = 1097;  
					yyal[159] = 1097;  
					yyal[160] = 1097;  
					yyal[161] = 1097;  
					yyal[162] = 1098;  
					yyal[163] = 1113;  
					yyal[164] = 1117;  
					yyal[165] = 1121;  
					yyal[166] = 1121;  
					yyal[167] = 1121;  
					yyal[168] = 1121;  
					yyal[169] = 1121;  
					yyal[170] = 1121;  
					yyal[171] = 1121;  
					yyal[172] = 1121;  
					yyal[173] = 1126;  
					yyal[174] = 1126;  
					yyal[175] = 1126;  
					yyal[176] = 1127;  
					yyal[177] = 1127;  
					yyal[178] = 1127;  
					yyal[179] = 1142;  
					yyal[180] = 1142;  
					yyal[181] = 1142;  
					yyal[182] = 1173;  
					yyal[183] = 1173;  
					yyal[184] = 1173;  
					yyal[185] = 1173;  
					yyal[186] = 1173;  
					yyal[187] = 1173;  
					yyal[188] = 1173;  
					yyal[189] = 1205;  
					yyal[190] = 1205;  
					yyal[191] = 1205;  
					yyal[192] = 1205;  
					yyal[193] = 1241;  
					yyal[194] = 1273;  
					yyal[195] = 1273;  
					yyal[196] = 1273;  
					yyal[197] = 1308;  
					yyal[198] = 1343;  
					yyal[199] = 1344;  
					yyal[200] = 1345;  
					yyal[201] = 1367;  
					yyal[202] = 1367;  
					yyal[203] = 1368;  
					yyal[204] = 1399;  
					yyal[205] = 1399;  
					yyal[206] = 1399;  
					yyal[207] = 1418;  
					yyal[208] = 1434;  
					yyal[209] = 1448;  
					yyal[210] = 1458;  
					yyal[211] = 1466;  
					yyal[212] = 1473;  
					yyal[213] = 1479;  
					yyal[214] = 1484;  
					yyal[215] = 1488;  
					yyal[216] = 1488;  
					yyal[217] = 1488;  
					yyal[218] = 1519;  
					yyal[219] = 1545;  
					yyal[220] = 1571;  
					yyal[221] = 1597;  
					yyal[222] = 1597;  
					yyal[223] = 1597;  
					yyal[224] = 1631;  
					yyal[225] = 1631;  
					yyal[226] = 1632;  
					yyal[227] = 1633;  
					yyal[228] = 1669;  
					yyal[229] = 1700;  
					yyal[230] = 1715;  
					yyal[231] = 1715;  
					yyal[232] = 1715;  
					yyal[233] = 1716;  
					yyal[234] = 1717;  
					yyal[235] = 1718;  
					yyal[236] = 1720;  
					yyal[237] = 1721;  
					yyal[238] = 1741;  
					yyal[239] = 1741;  
					yyal[240] = 1741;  
					yyal[241] = 1741;  
					yyal[242] = 1741;  
					yyal[243] = 1741;  
					yyal[244] = 1741;  
					yyal[245] = 1741;  
					yyal[246] = 1741;  
					yyal[247] = 1775;  
					yyal[248] = 1776;  
					yyal[249] = 1776;  
					yyal[250] = 1776;  
					yyal[251] = 1776;  
					yyal[252] = 1776;  
					yyal[253] = 1776;  
					yyal[254] = 1776;  
					yyal[255] = 1777;  
					yyal[256] = 1779;  
					yyal[257] = 1779;  
					yyal[258] = 1779;  
					yyal[259] = 1779;  
					yyal[260] = 1815;  
					yyal[261] = 1846;  
					yyal[262] = 1877;  
					yyal[263] = 1877;  
					yyal[264] = 1908;  
					yyal[265] = 1908;  
					yyal[266] = 1908;  
					yyal[267] = 1908;  
					yyal[268] = 1939;  
					yyal[269] = 1939;  
					yyal[270] = 1939;  
					yyal[271] = 1970;  
					yyal[272] = 1970;  
					yyal[273] = 1970;  
					yyal[274] = 1970;  
					yyal[275] = 1970;  
					yyal[276] = 2001;  
					yyal[277] = 2001;  
					yyal[278] = 2001;  
					yyal[279] = 2032;  
					yyal[280] = 2063;  
					yyal[281] = 2094;  
					yyal[282] = 2125;  
					yyal[283] = 2156;  
					yyal[284] = 2157;  
					yyal[285] = 2158;  
					yyal[286] = 2192;  
					yyal[287] = 2226;  
					yyal[288] = 2226;  
					yyal[289] = 2259;  
					yyal[290] = 2259;  
					yyal[291] = 2259;  
					yyal[292] = 2259;  
					yyal[293] = 2259;  
					yyal[294] = 2259;  
					yyal[295] = 2259;  
					yyal[296] = 2273;  
					yyal[297] = 2287;  
					yyal[298] = 2292;  
					yyal[299] = 2292;  
					yyal[300] = 2293;  
					yyal[301] = 2326;  
					yyal[302] = 2326;  
					yyal[303] = 2326;  
					yyal[304] = 2360;  
					yyal[305] = 2360;  
					yyal[306] = 2360;  
					yyal[307] = 2360;  
					yyal[308] = 2361;  
					yyal[309] = 2361;  
					yyal[310] = 2380;  
					yyal[311] = 2396;  
					yyal[312] = 2410;  
					yyal[313] = 2420;  
					yyal[314] = 2428;  
					yyal[315] = 2435;  
					yyal[316] = 2441;  
					yyal[317] = 2446;  
					yyal[318] = 2446;  
					yyal[319] = 2446;  
					yyal[320] = 2447;  
					yyal[321] = 2448;  
					yyal[322] = 2480;  
					yyal[323] = 2482;  
					yyal[324] = 2483;  
					yyal[325] = 2484;  
					yyal[326] = 2484;  
					yyal[327] = 2484;  
					yyal[328] = 2484;  
					yyal[329] = 2485;  
					yyal[330] = 2485;  
					yyal[331] = 2485;  
					yyal[332] = 2485;  
					yyal[333] = 2485;  
					yyal[334] = 2486;  
					yyal[335] = 2486;  
					yyal[336] = 2486;  
					yyal[337] = 2486;  
					yyal[338] = 2486;  
					yyal[339] = 2499;  
					yyal[340] = 2500; 

					yyah = new int[yynstates];
					yyah[0] = 20;  
					yyah[1] = 44;  
					yyah[2] = 44;  
					yyah[3] = 68;  
					yyah[4] = 93;  
					yyah[5] = 102;  
					yyah[6] = 127;  
					yyah[7] = 127;  
					yyah[8] = 127;  
					yyah[9] = 127;  
					yyah[10] = 127;  
					yyah[11] = 127;  
					yyah[12] = 127;  
					yyah[13] = 127;  
					yyah[14] = 127;  
					yyah[15] = 149;  
					yyah[16] = 149;  
					yyah[17] = 150;  
					yyah[18] = 154;  
					yyah[19] = 154;  
					yyah[20] = 158;  
					yyah[21] = 162;  
					yyah[22] = 166;  
					yyah[23] = 167;  
					yyah[24] = 167;  
					yyah[25] = 167;  
					yyah[26] = 167;  
					yyah[27] = 167;  
					yyah[28] = 167;  
					yyah[29] = 167;  
					yyah[30] = 167;  
					yyah[31] = 167;  
					yyah[32] = 167;  
					yyah[33] = 167;  
					yyah[34] = 167;  
					yyah[35] = 167;  
					yyah[36] = 167;  
					yyah[37] = 167;  
					yyah[38] = 167;  
					yyah[39] = 167;  
					yyah[40] = 169;  
					yyah[41] = 169;  
					yyah[42] = 169;  
					yyah[43] = 169;  
					yyah[44] = 169;  
					yyah[45] = 169;  
					yyah[46] = 169;  
					yyah[47] = 169;  
					yyah[48] = 169;  
					yyah[49] = 169;  
					yyah[50] = 169;  
					yyah[51] = 169;  
					yyah[52] = 169;  
					yyah[53] = 169;  
					yyah[54] = 169;  
					yyah[55] = 169;  
					yyah[56] = 169;  
					yyah[57] = 169;  
					yyah[58] = 169;  
					yyah[59] = 169;  
					yyah[60] = 169;  
					yyah[61] = 169;  
					yyah[62] = 169;  
					yyah[63] = 169;  
					yyah[64] = 169;  
					yyah[65] = 169;  
					yyah[66] = 169;  
					yyah[67] = 173;  
					yyah[68] = 173;  
					yyah[69] = 175;  
					yyah[70] = 175;  
					yyah[71] = 183;  
					yyah[72] = 183;  
					yyah[73] = 207;  
					yyah[74] = 207;  
					yyah[75] = 207;  
					yyah[76] = 207;  
					yyah[77] = 208;  
					yyah[78] = 209;  
					yyah[79] = 211;  
					yyah[80] = 212;  
					yyah[81] = 213;  
					yyah[82] = 213;  
					yyah[83] = 214;  
					yyah[84] = 216;  
					yyah[85] = 216;  
					yyah[86] = 217;  
					yyah[87] = 217;  
					yyah[88] = 219;  
					yyah[89] = 219;  
					yyah[90] = 220;  
					yyah[91] = 220;  
					yyah[92] = 220;  
					yyah[93] = 220;  
					yyah[94] = 220;  
					yyah[95] = 220;  
					yyah[96] = 220;  
					yyah[97] = 220;  
					yyah[98] = 220;  
					yyah[99] = 220;  
					yyah[100] = 220;  
					yyah[101] = 246;  
					yyah[102] = 247;  
					yyah[103] = 268;  
					yyah[104] = 289;  
					yyah[105] = 289;  
					yyah[106] = 321;  
					yyah[107] = 321;  
					yyah[108] = 321;  
					yyah[109] = 355;  
					yyah[110] = 356;  
					yyah[111] = 369;  
					yyah[112] = 375;  
					yyah[113] = 375;  
					yyah[114] = 375;  
					yyah[115] = 375;  
					yyah[116] = 400;  
					yyah[117] = 400;  
					yyah[118] = 401;  
					yyah[119] = 403;  
					yyah[120] = 404;  
					yyah[121] = 460;  
					yyah[122] = 460;  
					yyah[123] = 486;  
					yyah[124] = 486;  
					yyah[125] = 486;  
					yyah[126] = 486;  
					yyah[127] = 486;  
					yyah[128] = 486;  
					yyah[129] = 487;  
					yyah[130] = 487;  
					yyah[131] = 494;  
					yyah[132] = 499;  
					yyah[133] = 500;  
					yyah[134] = 534;  
					yyah[135] = 570;  
					yyah[136] = 606;  
					yyah[137] = 607;  
					yyah[138] = 647;  
					yyah[139] = 648;  
					yyah[140] = 684;  
					yyah[141] = 688;  
					yyah[142] = 688;  
					yyah[143] = 692;  
					yyah[144] = 696;  
					yyah[145] = 730;  
					yyah[146] = 761;  
					yyah[147] = 796;  
					yyah[148] = 827;  
					yyah[149] = 858;  
					yyah[150] = 899;  
					yyah[151] = 940;  
					yyah[152] = 981;  
					yyah[153] = 1022;  
					yyah[154] = 1063;  
					yyah[155] = 1063;  
					yyah[156] = 1095;  
					yyah[157] = 1096;  
					yyah[158] = 1096;  
					yyah[159] = 1096;  
					yyah[160] = 1096;  
					yyah[161] = 1097;  
					yyah[162] = 1112;  
					yyah[163] = 1116;  
					yyah[164] = 1120;  
					yyah[165] = 1120;  
					yyah[166] = 1120;  
					yyah[167] = 1120;  
					yyah[168] = 1120;  
					yyah[169] = 1120;  
					yyah[170] = 1120;  
					yyah[171] = 1120;  
					yyah[172] = 1125;  
					yyah[173] = 1125;  
					yyah[174] = 1125;  
					yyah[175] = 1126;  
					yyah[176] = 1126;  
					yyah[177] = 1126;  
					yyah[178] = 1141;  
					yyah[179] = 1141;  
					yyah[180] = 1141;  
					yyah[181] = 1172;  
					yyah[182] = 1172;  
					yyah[183] = 1172;  
					yyah[184] = 1172;  
					yyah[185] = 1172;  
					yyah[186] = 1172;  
					yyah[187] = 1172;  
					yyah[188] = 1204;  
					yyah[189] = 1204;  
					yyah[190] = 1204;  
					yyah[191] = 1204;  
					yyah[192] = 1240;  
					yyah[193] = 1272;  
					yyah[194] = 1272;  
					yyah[195] = 1272;  
					yyah[196] = 1307;  
					yyah[197] = 1342;  
					yyah[198] = 1343;  
					yyah[199] = 1344;  
					yyah[200] = 1366;  
					yyah[201] = 1366;  
					yyah[202] = 1367;  
					yyah[203] = 1398;  
					yyah[204] = 1398;  
					yyah[205] = 1398;  
					yyah[206] = 1417;  
					yyah[207] = 1433;  
					yyah[208] = 1447;  
					yyah[209] = 1457;  
					yyah[210] = 1465;  
					yyah[211] = 1472;  
					yyah[212] = 1478;  
					yyah[213] = 1483;  
					yyah[214] = 1487;  
					yyah[215] = 1487;  
					yyah[216] = 1487;  
					yyah[217] = 1518;  
					yyah[218] = 1544;  
					yyah[219] = 1570;  
					yyah[220] = 1596;  
					yyah[221] = 1596;  
					yyah[222] = 1596;  
					yyah[223] = 1630;  
					yyah[224] = 1630;  
					yyah[225] = 1631;  
					yyah[226] = 1632;  
					yyah[227] = 1668;  
					yyah[228] = 1699;  
					yyah[229] = 1714;  
					yyah[230] = 1714;  
					yyah[231] = 1714;  
					yyah[232] = 1715;  
					yyah[233] = 1716;  
					yyah[234] = 1717;  
					yyah[235] = 1719;  
					yyah[236] = 1720;  
					yyah[237] = 1740;  
					yyah[238] = 1740;  
					yyah[239] = 1740;  
					yyah[240] = 1740;  
					yyah[241] = 1740;  
					yyah[242] = 1740;  
					yyah[243] = 1740;  
					yyah[244] = 1740;  
					yyah[245] = 1740;  
					yyah[246] = 1774;  
					yyah[247] = 1775;  
					yyah[248] = 1775;  
					yyah[249] = 1775;  
					yyah[250] = 1775;  
					yyah[251] = 1775;  
					yyah[252] = 1775;  
					yyah[253] = 1775;  
					yyah[254] = 1776;  
					yyah[255] = 1778;  
					yyah[256] = 1778;  
					yyah[257] = 1778;  
					yyah[258] = 1778;  
					yyah[259] = 1814;  
					yyah[260] = 1845;  
					yyah[261] = 1876;  
					yyah[262] = 1876;  
					yyah[263] = 1907;  
					yyah[264] = 1907;  
					yyah[265] = 1907;  
					yyah[266] = 1907;  
					yyah[267] = 1938;  
					yyah[268] = 1938;  
					yyah[269] = 1938;  
					yyah[270] = 1969;  
					yyah[271] = 1969;  
					yyah[272] = 1969;  
					yyah[273] = 1969;  
					yyah[274] = 1969;  
					yyah[275] = 2000;  
					yyah[276] = 2000;  
					yyah[277] = 2000;  
					yyah[278] = 2031;  
					yyah[279] = 2062;  
					yyah[280] = 2093;  
					yyah[281] = 2124;  
					yyah[282] = 2155;  
					yyah[283] = 2156;  
					yyah[284] = 2157;  
					yyah[285] = 2191;  
					yyah[286] = 2225;  
					yyah[287] = 2225;  
					yyah[288] = 2258;  
					yyah[289] = 2258;  
					yyah[290] = 2258;  
					yyah[291] = 2258;  
					yyah[292] = 2258;  
					yyah[293] = 2258;  
					yyah[294] = 2258;  
					yyah[295] = 2272;  
					yyah[296] = 2286;  
					yyah[297] = 2291;  
					yyah[298] = 2291;  
					yyah[299] = 2292;  
					yyah[300] = 2325;  
					yyah[301] = 2325;  
					yyah[302] = 2325;  
					yyah[303] = 2359;  
					yyah[304] = 2359;  
					yyah[305] = 2359;  
					yyah[306] = 2359;  
					yyah[307] = 2360;  
					yyah[308] = 2360;  
					yyah[309] = 2379;  
					yyah[310] = 2395;  
					yyah[311] = 2409;  
					yyah[312] = 2419;  
					yyah[313] = 2427;  
					yyah[314] = 2434;  
					yyah[315] = 2440;  
					yyah[316] = 2445;  
					yyah[317] = 2445;  
					yyah[318] = 2445;  
					yyah[319] = 2446;  
					yyah[320] = 2447;  
					yyah[321] = 2479;  
					yyah[322] = 2481;  
					yyah[323] = 2482;  
					yyah[324] = 2483;  
					yyah[325] = 2483;  
					yyah[326] = 2483;  
					yyah[327] = 2483;  
					yyah[328] = 2484;  
					yyah[329] = 2484;  
					yyah[330] = 2484;  
					yyah[331] = 2484;  
					yyah[332] = 2484;  
					yyah[333] = 2485;  
					yyah[334] = 2485;  
					yyah[335] = 2485;  
					yyah[336] = 2485;  
					yyah[337] = 2485;  
					yyah[338] = 2498;  
					yyah[339] = 2499;  
					yyah[340] = 2499; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 18;  
					yygl[2] = 23;  
					yygl[3] = 23;  
					yygl[4] = 28;  
					yygl[5] = 33;  
					yygl[6] = 34;  
					yygl[7] = 35;  
					yygl[8] = 35;  
					yygl[9] = 35;  
					yygl[10] = 35;  
					yygl[11] = 35;  
					yygl[12] = 35;  
					yygl[13] = 35;  
					yygl[14] = 35;  
					yygl[15] = 35;  
					yygl[16] = 51;  
					yygl[17] = 51;  
					yygl[18] = 51;  
					yygl[19] = 54;  
					yygl[20] = 54;  
					yygl[21] = 57;  
					yygl[22] = 60;  
					yygl[23] = 63;  
					yygl[24] = 64;  
					yygl[25] = 64;  
					yygl[26] = 64;  
					yygl[27] = 64;  
					yygl[28] = 64;  
					yygl[29] = 64;  
					yygl[30] = 64;  
					yygl[31] = 64;  
					yygl[32] = 64;  
					yygl[33] = 64;  
					yygl[34] = 64;  
					yygl[35] = 64;  
					yygl[36] = 64;  
					yygl[37] = 64;  
					yygl[38] = 64;  
					yygl[39] = 64;  
					yygl[40] = 64;  
					yygl[41] = 65;  
					yygl[42] = 65;  
					yygl[43] = 65;  
					yygl[44] = 65;  
					yygl[45] = 65;  
					yygl[46] = 65;  
					yygl[47] = 65;  
					yygl[48] = 65;  
					yygl[49] = 65;  
					yygl[50] = 65;  
					yygl[51] = 65;  
					yygl[52] = 65;  
					yygl[53] = 65;  
					yygl[54] = 65;  
					yygl[55] = 65;  
					yygl[56] = 65;  
					yygl[57] = 65;  
					yygl[58] = 65;  
					yygl[59] = 65;  
					yygl[60] = 65;  
					yygl[61] = 65;  
					yygl[62] = 65;  
					yygl[63] = 65;  
					yygl[64] = 65;  
					yygl[65] = 65;  
					yygl[66] = 65;  
					yygl[67] = 65;  
					yygl[68] = 66;  
					yygl[69] = 66;  
					yygl[70] = 67;  
					yygl[71] = 67;  
					yygl[72] = 75;  
					yygl[73] = 75;  
					yygl[74] = 81;  
					yygl[75] = 81;  
					yygl[76] = 81;  
					yygl[77] = 81;  
					yygl[78] = 81;  
					yygl[79] = 81;  
					yygl[80] = 81;  
					yygl[81] = 81;  
					yygl[82] = 81;  
					yygl[83] = 81;  
					yygl[84] = 81;  
					yygl[85] = 82;  
					yygl[86] = 82;  
					yygl[87] = 83;  
					yygl[88] = 83;  
					yygl[89] = 83;  
					yygl[90] = 83;  
					yygl[91] = 83;  
					yygl[92] = 83;  
					yygl[93] = 83;  
					yygl[94] = 83;  
					yygl[95] = 83;  
					yygl[96] = 83;  
					yygl[97] = 83;  
					yygl[98] = 83;  
					yygl[99] = 83;  
					yygl[100] = 83;  
					yygl[101] = 83;  
					yygl[102] = 84;  
					yygl[103] = 84;  
					yygl[104] = 101;  
					yygl[105] = 118;  
					yygl[106] = 118;  
					yygl[107] = 132;  
					yygl[108] = 132;  
					yygl[109] = 132;  
					yygl[110] = 144;  
					yygl[111] = 144;  
					yygl[112] = 151;  
					yygl[113] = 152;  
					yygl[114] = 152;  
					yygl[115] = 152;  
					yygl[116] = 152;  
					yygl[117] = 158;  
					yygl[118] = 158;  
					yygl[119] = 158;  
					yygl[120] = 158;  
					yygl[121] = 158;  
					yygl[122] = 158;  
					yygl[123] = 158;  
					yygl[124] = 163;  
					yygl[125] = 163;  
					yygl[126] = 163;  
					yygl[127] = 163;  
					yygl[128] = 163;  
					yygl[129] = 163;  
					yygl[130] = 163;  
					yygl[131] = 163;  
					yygl[132] = 163;  
					yygl[133] = 164;  
					yygl[134] = 164;  
					yygl[135] = 165;  
					yygl[136] = 177;  
					yygl[137] = 189;  
					yygl[138] = 189;  
					yygl[139] = 190;  
					yygl[140] = 190;  
					yygl[141] = 202;  
					yygl[142] = 205;  
					yygl[143] = 205;  
					yygl[144] = 208;  
					yygl[145] = 211;  
					yygl[146] = 223;  
					yygl[147] = 244;  
					yygl[148] = 244;  
					yygl[149] = 265;  
					yygl[150] = 286;  
					yygl[151] = 286;  
					yygl[152] = 286;  
					yygl[153] = 286;  
					yygl[154] = 286;  
					yygl[155] = 286;  
					yygl[156] = 286;  
					yygl[157] = 287;  
					yygl[158] = 287;  
					yygl[159] = 287;  
					yygl[160] = 287;  
					yygl[161] = 287;  
					yygl[162] = 287;  
					yygl[163] = 294;  
					yygl[164] = 297;  
					yygl[165] = 300;  
					yygl[166] = 300;  
					yygl[167] = 300;  
					yygl[168] = 300;  
					yygl[169] = 300;  
					yygl[170] = 300;  
					yygl[171] = 300;  
					yygl[172] = 300;  
					yygl[173] = 303;  
					yygl[174] = 303;  
					yygl[175] = 303;  
					yygl[176] = 303;  
					yygl[177] = 303;  
					yygl[178] = 303;  
					yygl[179] = 308;  
					yygl[180] = 308;  
					yygl[181] = 308;  
					yygl[182] = 329;  
					yygl[183] = 329;  
					yygl[184] = 329;  
					yygl[185] = 329;  
					yygl[186] = 329;  
					yygl[187] = 329;  
					yygl[188] = 329;  
					yygl[189] = 344;  
					yygl[190] = 344;  
					yygl[191] = 344;  
					yygl[192] = 344;  
					yygl[193] = 356;  
					yygl[194] = 371;  
					yygl[195] = 371;  
					yygl[196] = 371;  
					yygl[197] = 384;  
					yygl[198] = 397;  
					yygl[199] = 397;  
					yygl[200] = 397;  
					yygl[201] = 398;  
					yygl[202] = 398;  
					yygl[203] = 398;  
					yygl[204] = 409;  
					yygl[205] = 409;  
					yygl[206] = 409;  
					yygl[207] = 410;  
					yygl[208] = 411;  
					yygl[209] = 412;  
					yygl[210] = 413;  
					yygl[211] = 413;  
					yygl[212] = 413;  
					yygl[213] = 413;  
					yygl[214] = 413;  
					yygl[215] = 413;  
					yygl[216] = 413;  
					yygl[217] = 413;  
					yygl[218] = 434;  
					yygl[219] = 434;  
					yygl[220] = 434;  
					yygl[221] = 434;  
					yygl[222] = 434;  
					yygl[223] = 434;  
					yygl[224] = 446;  
					yygl[225] = 446;  
					yygl[226] = 446;  
					yygl[227] = 446;  
					yygl[228] = 458;  
					yygl[229] = 472;  
					yygl[230] = 479;  
					yygl[231] = 479;  
					yygl[232] = 479;  
					yygl[233] = 479;  
					yygl[234] = 479;  
					yygl[235] = 479;  
					yygl[236] = 479;  
					yygl[237] = 479;  
					yygl[238] = 495;  
					yygl[239] = 495;  
					yygl[240] = 495;  
					yygl[241] = 495;  
					yygl[242] = 495;  
					yygl[243] = 495;  
					yygl[244] = 495;  
					yygl[245] = 495;  
					yygl[246] = 495;  
					yygl[247] = 496;  
					yygl[248] = 496;  
					yygl[249] = 496;  
					yygl[250] = 496;  
					yygl[251] = 496;  
					yygl[252] = 496;  
					yygl[253] = 496;  
					yygl[254] = 496;  
					yygl[255] = 496;  
					yygl[256] = 496;  
					yygl[257] = 496;  
					yygl[258] = 496;  
					yygl[259] = 496;  
					yygl[260] = 508;  
					yygl[261] = 529;  
					yygl[262] = 550;  
					yygl[263] = 550;  
					yygl[264] = 561;  
					yygl[265] = 561;  
					yygl[266] = 561;  
					yygl[267] = 561;  
					yygl[268] = 573;  
					yygl[269] = 573;  
					yygl[270] = 573;  
					yygl[271] = 586;  
					yygl[272] = 586;  
					yygl[273] = 586;  
					yygl[274] = 586;  
					yygl[275] = 586;  
					yygl[276] = 600;  
					yygl[277] = 600;  
					yygl[278] = 600;  
					yygl[279] = 615;  
					yygl[280] = 631;  
					yygl[281] = 648;  
					yygl[282] = 666;  
					yygl[283] = 685;  
					yygl[284] = 685;  
					yygl[285] = 685;  
					yygl[286] = 697;  
					yygl[287] = 709;  
					yygl[288] = 709;  
					yygl[289] = 710;  
					yygl[290] = 710;  
					yygl[291] = 710;  
					yygl[292] = 710;  
					yygl[293] = 710;  
					yygl[294] = 710;  
					yygl[295] = 710;  
					yygl[296] = 718;  
					yygl[297] = 726;  
					yygl[298] = 729;  
					yygl[299] = 729;  
					yygl[300] = 729;  
					yygl[301] = 744;  
					yygl[302] = 744;  
					yygl[303] = 744;  
					yygl[304] = 756;  
					yygl[305] = 756;  
					yygl[306] = 756;  
					yygl[307] = 756;  
					yygl[308] = 756;  
					yygl[309] = 756;  
					yygl[310] = 757;  
					yygl[311] = 758;  
					yygl[312] = 759;  
					yygl[313] = 760;  
					yygl[314] = 760;  
					yygl[315] = 760;  
					yygl[316] = 760;  
					yygl[317] = 760;  
					yygl[318] = 760;  
					yygl[319] = 760;  
					yygl[320] = 760;  
					yygl[321] = 760;  
					yygl[322] = 774;  
					yygl[323] = 774;  
					yygl[324] = 774;  
					yygl[325] = 774;  
					yygl[326] = 774;  
					yygl[327] = 774;  
					yygl[328] = 774;  
					yygl[329] = 774;  
					yygl[330] = 774;  
					yygl[331] = 774;  
					yygl[332] = 774;  
					yygl[333] = 774;  
					yygl[334] = 774;  
					yygl[335] = 774;  
					yygl[336] = 774;  
					yygl[337] = 774;  
					yygl[338] = 774;  
					yygl[339] = 781;  
					yygl[340] = 781; 

					yygh = new int[yynstates];
					yygh[0] = 17;  
					yygh[1] = 22;  
					yygh[2] = 22;  
					yygh[3] = 27;  
					yygh[4] = 32;  
					yygh[5] = 33;  
					yygh[6] = 34;  
					yygh[7] = 34;  
					yygh[8] = 34;  
					yygh[9] = 34;  
					yygh[10] = 34;  
					yygh[11] = 34;  
					yygh[12] = 34;  
					yygh[13] = 34;  
					yygh[14] = 34;  
					yygh[15] = 50;  
					yygh[16] = 50;  
					yygh[17] = 50;  
					yygh[18] = 53;  
					yygh[19] = 53;  
					yygh[20] = 56;  
					yygh[21] = 59;  
					yygh[22] = 62;  
					yygh[23] = 63;  
					yygh[24] = 63;  
					yygh[25] = 63;  
					yygh[26] = 63;  
					yygh[27] = 63;  
					yygh[28] = 63;  
					yygh[29] = 63;  
					yygh[30] = 63;  
					yygh[31] = 63;  
					yygh[32] = 63;  
					yygh[33] = 63;  
					yygh[34] = 63;  
					yygh[35] = 63;  
					yygh[36] = 63;  
					yygh[37] = 63;  
					yygh[38] = 63;  
					yygh[39] = 63;  
					yygh[40] = 64;  
					yygh[41] = 64;  
					yygh[42] = 64;  
					yygh[43] = 64;  
					yygh[44] = 64;  
					yygh[45] = 64;  
					yygh[46] = 64;  
					yygh[47] = 64;  
					yygh[48] = 64;  
					yygh[49] = 64;  
					yygh[50] = 64;  
					yygh[51] = 64;  
					yygh[52] = 64;  
					yygh[53] = 64;  
					yygh[54] = 64;  
					yygh[55] = 64;  
					yygh[56] = 64;  
					yygh[57] = 64;  
					yygh[58] = 64;  
					yygh[59] = 64;  
					yygh[60] = 64;  
					yygh[61] = 64;  
					yygh[62] = 64;  
					yygh[63] = 64;  
					yygh[64] = 64;  
					yygh[65] = 64;  
					yygh[66] = 64;  
					yygh[67] = 65;  
					yygh[68] = 65;  
					yygh[69] = 66;  
					yygh[70] = 66;  
					yygh[71] = 74;  
					yygh[72] = 74;  
					yygh[73] = 80;  
					yygh[74] = 80;  
					yygh[75] = 80;  
					yygh[76] = 80;  
					yygh[77] = 80;  
					yygh[78] = 80;  
					yygh[79] = 80;  
					yygh[80] = 80;  
					yygh[81] = 80;  
					yygh[82] = 80;  
					yygh[83] = 80;  
					yygh[84] = 81;  
					yygh[85] = 81;  
					yygh[86] = 82;  
					yygh[87] = 82;  
					yygh[88] = 82;  
					yygh[89] = 82;  
					yygh[90] = 82;  
					yygh[91] = 82;  
					yygh[92] = 82;  
					yygh[93] = 82;  
					yygh[94] = 82;  
					yygh[95] = 82;  
					yygh[96] = 82;  
					yygh[97] = 82;  
					yygh[98] = 82;  
					yygh[99] = 82;  
					yygh[100] = 82;  
					yygh[101] = 83;  
					yygh[102] = 83;  
					yygh[103] = 100;  
					yygh[104] = 117;  
					yygh[105] = 117;  
					yygh[106] = 131;  
					yygh[107] = 131;  
					yygh[108] = 131;  
					yygh[109] = 143;  
					yygh[110] = 143;  
					yygh[111] = 150;  
					yygh[112] = 151;  
					yygh[113] = 151;  
					yygh[114] = 151;  
					yygh[115] = 151;  
					yygh[116] = 157;  
					yygh[117] = 157;  
					yygh[118] = 157;  
					yygh[119] = 157;  
					yygh[120] = 157;  
					yygh[121] = 157;  
					yygh[122] = 157;  
					yygh[123] = 162;  
					yygh[124] = 162;  
					yygh[125] = 162;  
					yygh[126] = 162;  
					yygh[127] = 162;  
					yygh[128] = 162;  
					yygh[129] = 162;  
					yygh[130] = 162;  
					yygh[131] = 162;  
					yygh[132] = 163;  
					yygh[133] = 163;  
					yygh[134] = 164;  
					yygh[135] = 176;  
					yygh[136] = 188;  
					yygh[137] = 188;  
					yygh[138] = 189;  
					yygh[139] = 189;  
					yygh[140] = 201;  
					yygh[141] = 204;  
					yygh[142] = 204;  
					yygh[143] = 207;  
					yygh[144] = 210;  
					yygh[145] = 222;  
					yygh[146] = 243;  
					yygh[147] = 243;  
					yygh[148] = 264;  
					yygh[149] = 285;  
					yygh[150] = 285;  
					yygh[151] = 285;  
					yygh[152] = 285;  
					yygh[153] = 285;  
					yygh[154] = 285;  
					yygh[155] = 285;  
					yygh[156] = 286;  
					yygh[157] = 286;  
					yygh[158] = 286;  
					yygh[159] = 286;  
					yygh[160] = 286;  
					yygh[161] = 286;  
					yygh[162] = 293;  
					yygh[163] = 296;  
					yygh[164] = 299;  
					yygh[165] = 299;  
					yygh[166] = 299;  
					yygh[167] = 299;  
					yygh[168] = 299;  
					yygh[169] = 299;  
					yygh[170] = 299;  
					yygh[171] = 299;  
					yygh[172] = 302;  
					yygh[173] = 302;  
					yygh[174] = 302;  
					yygh[175] = 302;  
					yygh[176] = 302;  
					yygh[177] = 302;  
					yygh[178] = 307;  
					yygh[179] = 307;  
					yygh[180] = 307;  
					yygh[181] = 328;  
					yygh[182] = 328;  
					yygh[183] = 328;  
					yygh[184] = 328;  
					yygh[185] = 328;  
					yygh[186] = 328;  
					yygh[187] = 328;  
					yygh[188] = 343;  
					yygh[189] = 343;  
					yygh[190] = 343;  
					yygh[191] = 343;  
					yygh[192] = 355;  
					yygh[193] = 370;  
					yygh[194] = 370;  
					yygh[195] = 370;  
					yygh[196] = 383;  
					yygh[197] = 396;  
					yygh[198] = 396;  
					yygh[199] = 396;  
					yygh[200] = 397;  
					yygh[201] = 397;  
					yygh[202] = 397;  
					yygh[203] = 408;  
					yygh[204] = 408;  
					yygh[205] = 408;  
					yygh[206] = 409;  
					yygh[207] = 410;  
					yygh[208] = 411;  
					yygh[209] = 412;  
					yygh[210] = 412;  
					yygh[211] = 412;  
					yygh[212] = 412;  
					yygh[213] = 412;  
					yygh[214] = 412;  
					yygh[215] = 412;  
					yygh[216] = 412;  
					yygh[217] = 433;  
					yygh[218] = 433;  
					yygh[219] = 433;  
					yygh[220] = 433;  
					yygh[221] = 433;  
					yygh[222] = 433;  
					yygh[223] = 445;  
					yygh[224] = 445;  
					yygh[225] = 445;  
					yygh[226] = 445;  
					yygh[227] = 457;  
					yygh[228] = 471;  
					yygh[229] = 478;  
					yygh[230] = 478;  
					yygh[231] = 478;  
					yygh[232] = 478;  
					yygh[233] = 478;  
					yygh[234] = 478;  
					yygh[235] = 478;  
					yygh[236] = 478;  
					yygh[237] = 494;  
					yygh[238] = 494;  
					yygh[239] = 494;  
					yygh[240] = 494;  
					yygh[241] = 494;  
					yygh[242] = 494;  
					yygh[243] = 494;  
					yygh[244] = 494;  
					yygh[245] = 494;  
					yygh[246] = 495;  
					yygh[247] = 495;  
					yygh[248] = 495;  
					yygh[249] = 495;  
					yygh[250] = 495;  
					yygh[251] = 495;  
					yygh[252] = 495;  
					yygh[253] = 495;  
					yygh[254] = 495;  
					yygh[255] = 495;  
					yygh[256] = 495;  
					yygh[257] = 495;  
					yygh[258] = 495;  
					yygh[259] = 507;  
					yygh[260] = 528;  
					yygh[261] = 549;  
					yygh[262] = 549;  
					yygh[263] = 560;  
					yygh[264] = 560;  
					yygh[265] = 560;  
					yygh[266] = 560;  
					yygh[267] = 572;  
					yygh[268] = 572;  
					yygh[269] = 572;  
					yygh[270] = 585;  
					yygh[271] = 585;  
					yygh[272] = 585;  
					yygh[273] = 585;  
					yygh[274] = 585;  
					yygh[275] = 599;  
					yygh[276] = 599;  
					yygh[277] = 599;  
					yygh[278] = 614;  
					yygh[279] = 630;  
					yygh[280] = 647;  
					yygh[281] = 665;  
					yygh[282] = 684;  
					yygh[283] = 684;  
					yygh[284] = 684;  
					yygh[285] = 696;  
					yygh[286] = 708;  
					yygh[287] = 708;  
					yygh[288] = 709;  
					yygh[289] = 709;  
					yygh[290] = 709;  
					yygh[291] = 709;  
					yygh[292] = 709;  
					yygh[293] = 709;  
					yygh[294] = 709;  
					yygh[295] = 717;  
					yygh[296] = 725;  
					yygh[297] = 728;  
					yygh[298] = 728;  
					yygh[299] = 728;  
					yygh[300] = 743;  
					yygh[301] = 743;  
					yygh[302] = 743;  
					yygh[303] = 755;  
					yygh[304] = 755;  
					yygh[305] = 755;  
					yygh[306] = 755;  
					yygh[307] = 755;  
					yygh[308] = 755;  
					yygh[309] = 756;  
					yygh[310] = 757;  
					yygh[311] = 758;  
					yygh[312] = 759;  
					yygh[313] = 759;  
					yygh[314] = 759;  
					yygh[315] = 759;  
					yygh[316] = 759;  
					yygh[317] = 759;  
					yygh[318] = 759;  
					yygh[319] = 759;  
					yygh[320] = 759;  
					yygh[321] = 773;  
					yygh[322] = 773;  
					yygh[323] = 773;  
					yygh[324] = 773;  
					yygh[325] = 773;  
					yygh[326] = 773;  
					yygh[327] = 773;  
					yygh[328] = 773;  
					yygh[329] = 773;  
					yygh[330] = 773;  
					yygh[331] = 773;  
					yygh[332] = 773;  
					yygh[333] = 773;  
					yygh[334] = 773;  
					yygh[335] = 773;  
					yygh[336] = 773;  
					yygh[337] = 773;  
					yygh[338] = 780;  
					yygh[339] = 780;  
					yygh[340] = 780; 

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
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
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
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
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
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++;
        }

        public bool yyact(int state, int sym, ref int act)
        {
            int k = yyal[state];
            while (k <= yyah[state] && yya[k].sym != sym) k++;
            if (k > yyah[state]) return false;
            act = yya[k].act;
            return true;
        }
        public bool yygoto(int state, int sym, ref int nstate)
        {
            int k = yygl[state];
            while (k <= yygh[state] && yyg[k].sym != sym) k++;
            if (k > yygh[state]) return false;
            nstate = yyg[k].act;
            return true;
        }

        public void yyerror(string s)
        {
            System.Console.Write(s);
        }

        int yylexpos = -1;
        string yylval = "";

        public int yylex()
        {
            yylexpos++;
            if (yylexpos >= TokenList.Count)
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

        public bool yyparse()
        {

            parse:

            yysp++;
            if (yysp >= yymaxdepth)
            {
                yyerror("yyparse stack overflow");
                goto abort;
            }

            yys[yysp] = yystate;
            yyv[yysp] = yyval;

            next:

            if (yyd[yystate] == 0 && yychar == -1)
            {
                yychar = yylex();
                if (yychar < 0) yychar = 0;
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
                while (yysp > 0 && !(yyact(yys[yysp], 255, ref yyn) && yyn > 0)) yysp--;

                if (yysp == 0) goto abort;
                yystate = yyn;
                goto parse;
            }
            else
            {
                if (yychar == 0) goto abort;
                yychar = -1; goto next;
            }

            shift:

            yystate = yyn;
            yychar = -1;
            yyval = yylval;
            if (yyerrflag > 0) yyerrflag--;
            goto parse;

            reduce:

            yyflag = yyfnone;
            yyaction(-yyn);
            int l = yyr[-yyn].len;
            for (int z = yysp; z > yysp - l; z--)
                yyv[z] = null;

            yysp -= l;
//            yysp -= yyr[-yyn].len;

            if (yygoto(yys[yysp], yyr[-yyn].sym, ref yyn)) yystate = yyn;

            switch (yyflag)
            {
                case 1: goto accept;
                case 2: goto abort;
                case 3: goto errlab;
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

        public bool ScannerOpt(string Input)
        {
            if (Input.Length == 0) return true;
            TokenList = new ArrayList();
            int pos = 0;
            while (1 == 1)
            {
                AToken lasttoken = FindTokenOpt(Input, pos);
                if (lasttoken.token == 0) break;
                if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
                pos += lasttoken.val.Length;
                if (Input.Length <= pos)
                    return true;
            }
            System.Console.WriteLine(Input);
            System.Console.WriteLine();
            System.Console.WriteLine("No matching token found!");
            return false;
        }
        public AToken FindTokenOpt(string Rest, int startpos)
        {
            Match m;
            int maxlength = 0;
            int besttoken = 0;
            AToken ret = new AToken();
            try
            {

                for (int idx = 0; idx < tList.Count; idx++)
                {
                    m = rList[idx].Match(Rest, startpos);
                    if (m.Success)
                    {
                        if (m.Value.Length > maxlength)
                        {
                            maxlength = m.Value.Length;
                            besttoken = tList[idx];
                            ret.token = besttoken;
                            if (besttoken != 0)
                                ret.val = m.Value;
                        }
                    }
                }

            }
            catch { }
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

			if (Regex.IsMatch(Rest,"^((,*[\\s\\t\\x00]*)?;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^((,*[\\s\\t\\x00]*)?;)").Value);}

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

			if (Regex.IsMatch(Rest,"^((,+[\\s\\t\\x00]*)*|:=)")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^((,+[\\s\\t\\x00]*)*|:=)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\})")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\})").Value);}

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

			if (Regex.IsMatch(Rest,"^(\\*[\\s\\t\\x00]*=)")){
				Results.Add (t_Char42Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\*[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+[\\s\\t\\x00]*=)")){
				Results.Add (t_Char43Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\+[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-[\\s\\t\\x00]*=)")){
				Results.Add (t_Char45Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\-[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(/[\\s\\t\\x00]*=)")){
				Results.Add (t_Char47Char61);
				ResultsV.Add(Regex.Match(Rest,"^(/[\\s\\t\\x00]*=)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))")){
				Results.Add (t_global);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))")){
				Results.Add (t_asset);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))")){
				Results.Add (t_object);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTION|RULES))")){
				Results.Add (t_function);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTION|RULES))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))")){
				Results.Add (t_ambigChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SAVE)")){
				Results.Add (t_ambigChar95commandChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SAVE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)MSPRITE)")){
				Results.Add (t_ambigChar95globalChar95synonymChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MSPRITE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DO)")){
				Results.Add (t_ambigChar95commandChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DO)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLAG[1-8]))")){
				Results.Add (t_ambigChar95skillChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLAG[1-8]))").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))")){
				Results.Add (t_fixed);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)").Value);}

			if (Regex.IsMatch(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")").Value);}

			if (Regex.IsMatch(Rest,"^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")){
				Results.Add (t_ignore);
				ResultsV.Add(Regex.Match(Rest,"^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))").Value);}

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
