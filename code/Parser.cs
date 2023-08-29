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
            rList.Add(new Regex("\\G((:=|(,+[\\s\\t\\x00]*)*))"));
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
            rList.Add(new Regex("\\G([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)"));
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

            //StreamReader in_s = File.OpenText(InputFilename);
            //string inputstream = in_s.ReadToEnd();
            //in_s.Close();
            string inputstream = File.ReadAllText(InputFilename, Encoding.ASCII);

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
         yyval = ""; //bogus keyword at EOF - just discard
         
       break;
							case    4 : 
         yyval = "";
         
       break;
							case    5 : 
         yyval = "";
         
       break;
							case    6 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    7 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = yyv[yysp-0];
         
       break;
							case   10 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   11 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
       break;
							case   13 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   14 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   15 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   19 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   20 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   21 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   22 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   23 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   24 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   25 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   26 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   27 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   28 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   29 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   30 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddListDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   36 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   37 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-3]);
         
       break;
							case   38 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   43 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   44 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   45 : 
         //yyval = yyv[yysp-1];
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   46 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Globals.AddParameter(yyv[yysp-2]);
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3]);
         
       break;
							case   52 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-2]);
         
       break;
							case   53 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-0]);
         
       break;
							case   54 : 
         yyval = "";
         
       break;
							case   55 : 
         yyval = yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-0];
         
       break;
							case   58 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   59 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddStringObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   60 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
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
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = "";
         
       break;
							case   67 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   68 :
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   69 :
                    Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
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
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   77 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   78 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   79 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = "";
         
       break;
							case   83 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-3]);
         
       break;
							case   84 : 
         //Capture and discard bogus code
         yyval = Actions.CreateInvalidInstruction(yyv[yysp-3]);
         
       break;
							case   85 : 
         yyval = yyv[yysp-1];
         
       break;
							case   86 : 
         //yyval = yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   87 : 
         yyval = "";
         
       break;
							case   88 : 
         yyval = yyv[yysp-0];
         
       break;
							case   89 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   90 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = "";
         
       break;
							case   98 : 
         yyval = yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  120 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  121 : 
         yyval = yyv[yysp-0];
         
       break;
							case  122 : 
         yyval = yyv[yysp-0];
         
       break;
							case  123 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  124 : 
         yyval = " != ";
         
       break;
							case  125 : 
         yyval = " == ";
         
       break;
							case  126 : 
         yyval = " < ";
         
       break;
							case  127 : 
         yyval = " <= ";
         
       break;
							case  128 : 
         yyval = " > ";
         
       break;
							case  129 : 
         yyval = " >= ";
         
       break;
							case  130 : 
         yyval = " + ";
         
       break;
							case  131 : 
         yyval = " - ";
         
       break;
							case  132 : 
         yyval = " % ";
         
       break;
							case  133 : 
         yyval = " * ";
         
       break;
							case  134 : 
         yyval = " / ";
         
       break;
							case  135 : 
         yyval = "!";
         
       break;
							case  136 : 
         yyval = "+";
         
       break;
							case  137 : 
         yyval = "-";
         
       break;
							case  138 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  139 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  140 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  141 : 
         yyval = " *= ";
         
       break;
							case  142 : 
         yyval = " += ";
         
       break;
							case  143 : 
         yyval = " -= ";
         
       break;
							case  144 : 
         yyval = " /= ";
         
       break;
							case  145 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  150 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  151 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  152 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  177 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  178 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  179 : 
         yyval = yyv[yysp-0];
         
       break;
							case  180 : 
         yyval = yyv[yysp-0];
         
       break;
							case  181 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  186 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  187 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  191 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  196 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  197 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  198 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  199 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  204 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  205 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  206 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  207 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  208 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  209 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  210 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  211 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  212 : 
         yyval = Formatter.FormatNull();
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  218 : 
         yyval = yyv[yysp-0];
         
       break;
							case  219 : 
         yyval = yyv[yysp-0];
         
       break;
							case  220 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  221 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  222 : 
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

					int yynacts   = 2823;
					int yyngotos  = 820;
					int yynstates = 347;
					int yynrules  = 222;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(297,54);yyac++; 
					yya[yyac] = new YYARec(0,-154 );yyac++; 
					yya[yyac] = new YYARec(258,-154 );yyac++; 
					yya[yyac] = new YYARec(260,-154 );yyac++; 
					yya[yyac] = new YYARec(261,-154 );yyac++; 
					yya[yyac] = new YYARec(263,-154 );yyac++; 
					yya[yyac] = new YYARec(266,-154 );yyac++; 
					yya[yyac] = new YYARec(269,-154 );yyac++; 
					yya[yyac] = new YYARec(270,-154 );yyac++; 
					yya[yyac] = new YYARec(271,-154 );yyac++; 
					yya[yyac] = new YYARec(272,-154 );yyac++; 
					yya[yyac] = new YYARec(273,-154 );yyac++; 
					yya[yyac] = new YYARec(275,-154 );yyac++; 
					yya[yyac] = new YYARec(276,-154 );yyac++; 
					yya[yyac] = new YYARec(277,-154 );yyac++; 
					yya[yyac] = new YYARec(278,-154 );yyac++; 
					yya[yyac] = new YYARec(279,-154 );yyac++; 
					yya[yyac] = new YYARec(280,-154 );yyac++; 
					yya[yyac] = new YYARec(281,-154 );yyac++; 
					yya[yyac] = new YYARec(282,-154 );yyac++; 
					yya[yyac] = new YYARec(283,-154 );yyac++; 
					yya[yyac] = new YYARec(284,-154 );yyac++; 
					yya[yyac] = new YYARec(285,-154 );yyac++; 
					yya[yyac] = new YYARec(286,-154 );yyac++; 
					yya[yyac] = new YYARec(287,-154 );yyac++; 
					yya[yyac] = new YYARec(289,-154 );yyac++; 
					yya[yyac] = new YYARec(290,-154 );yyac++; 
					yya[yyac] = new YYARec(291,-154 );yyac++; 
					yya[yyac] = new YYARec(292,-154 );yyac++; 
					yya[yyac] = new YYARec(293,-154 );yyac++; 
					yya[yyac] = new YYARec(298,-154 );yyac++; 
					yya[yyac] = new YYARec(299,-154 );yyac++; 
					yya[yyac] = new YYARec(300,-154 );yyac++; 
					yya[yyac] = new YYARec(301,-154 );yyac++; 
					yya[yyac] = new YYARec(302,-154 );yyac++; 
					yya[yyac] = new YYARec(303,-154 );yyac++; 
					yya[yyac] = new YYARec(304,-154 );yyac++; 
					yya[yyac] = new YYARec(305,-154 );yyac++; 
					yya[yyac] = new YYARec(306,-154 );yyac++; 
					yya[yyac] = new YYARec(307,-154 );yyac++; 
					yya[yyac] = new YYARec(308,-154 );yyac++; 
					yya[yyac] = new YYARec(309,-154 );yyac++; 
					yya[yyac] = new YYARec(310,-154 );yyac++; 
					yya[yyac] = new YYARec(311,-154 );yyac++; 
					yya[yyac] = new YYARec(312,-154 );yyac++; 
					yya[yyac] = new YYARec(313,-154 );yyac++; 
					yya[yyac] = new YYARec(314,-154 );yyac++; 
					yya[yyac] = new YYARec(315,-154 );yyac++; 
					yya[yyac] = new YYARec(316,-154 );yyac++; 
					yya[yyac] = new YYARec(317,-154 );yyac++; 
					yya[yyac] = new YYARec(318,-154 );yyac++; 
					yya[yyac] = new YYARec(319,-154 );yyac++; 
					yya[yyac] = new YYARec(320,-154 );yyac++; 
					yya[yyac] = new YYARec(321,-154 );yyac++; 
					yya[yyac] = new YYARec(322,-154 );yyac++; 
					yya[yyac] = new YYARec(323,-154 );yyac++; 
					yya[yyac] = new YYARec(324,-154 );yyac++; 
					yya[yyac] = new YYARec(325,-154 );yyac++; 
					yya[yyac] = new YYARec(326,-154 );yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,73);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(263,-44 );yyac++; 
					yya[yyac] = new YYARec(298,-44 );yyac++; 
					yya[yyac] = new YYARec(299,-44 );yyac++; 
					yya[yyac] = new YYARec(300,-44 );yyac++; 
					yya[yyac] = new YYARec(301,-44 );yyac++; 
					yya[yyac] = new YYARec(302,-44 );yyac++; 
					yya[yyac] = new YYARec(303,-44 );yyac++; 
					yya[yyac] = new YYARec(304,-44 );yyac++; 
					yya[yyac] = new YYARec(305,-44 );yyac++; 
					yya[yyac] = new YYARec(306,-44 );yyac++; 
					yya[yyac] = new YYARec(307,-44 );yyac++; 
					yya[yyac] = new YYARec(309,-44 );yyac++; 
					yya[yyac] = new YYARec(310,-44 );yyac++; 
					yya[yyac] = new YYARec(311,-44 );yyac++; 
					yya[yyac] = new YYARec(312,-44 );yyac++; 
					yya[yyac] = new YYARec(313,-44 );yyac++; 
					yya[yyac] = new YYARec(314,-44 );yyac++; 
					yya[yyac] = new YYARec(315,-44 );yyac++; 
					yya[yyac] = new YYARec(316,-44 );yyac++; 
					yya[yyac] = new YYARec(317,-44 );yyac++; 
					yya[yyac] = new YYARec(318,-44 );yyac++; 
					yya[yyac] = new YYARec(319,-44 );yyac++; 
					yya[yyac] = new YYARec(320,-44 );yyac++; 
					yya[yyac] = new YYARec(321,-44 );yyac++; 
					yya[yyac] = new YYARec(324,-44 );yyac++; 
					yya[yyac] = new YYARec(0,-198 );yyac++; 
					yya[yyac] = new YYARec(260,-198 );yyac++; 
					yya[yyac] = new YYARec(261,-198 );yyac++; 
					yya[yyac] = new YYARec(297,-198 );yyac++; 
					yya[yyac] = new YYARec(263,-41 );yyac++; 
					yya[yyac] = new YYARec(282,-41 );yyac++; 
					yya[yyac] = new YYARec(283,-41 );yyac++; 
					yya[yyac] = new YYARec(287,-41 );yyac++; 
					yya[yyac] = new YYARec(322,-41 );yyac++; 
					yya[yyac] = new YYARec(323,-41 );yyac++; 
					yya[yyac] = new YYARec(324,-41 );yyac++; 
					yya[yyac] = new YYARec(325,-41 );yyac++; 
					yya[yyac] = new YYARec(326,-41 );yyac++; 
					yya[yyac] = new YYARec(0,-201 );yyac++; 
					yya[yyac] = new YYARec(260,-201 );yyac++; 
					yya[yyac] = new YYARec(261,-201 );yyac++; 
					yya[yyac] = new YYARec(297,-201 );yyac++; 
					yya[yyac] = new YYARec(298,-57 );yyac++; 
					yya[yyac] = new YYARec(299,-57 );yyac++; 
					yya[yyac] = new YYARec(300,-57 );yyac++; 
					yya[yyac] = new YYARec(301,-57 );yyac++; 
					yya[yyac] = new YYARec(302,-57 );yyac++; 
					yya[yyac] = new YYARec(303,-57 );yyac++; 
					yya[yyac] = new YYARec(304,-57 );yyac++; 
					yya[yyac] = new YYARec(305,-57 );yyac++; 
					yya[yyac] = new YYARec(306,-57 );yyac++; 
					yya[yyac] = new YYARec(307,-57 );yyac++; 
					yya[yyac] = new YYARec(309,-57 );yyac++; 
					yya[yyac] = new YYARec(310,-57 );yyac++; 
					yya[yyac] = new YYARec(311,-57 );yyac++; 
					yya[yyac] = new YYARec(312,-57 );yyac++; 
					yya[yyac] = new YYARec(313,-57 );yyac++; 
					yya[yyac] = new YYARec(314,-57 );yyac++; 
					yya[yyac] = new YYARec(315,-57 );yyac++; 
					yya[yyac] = new YYARec(316,-57 );yyac++; 
					yya[yyac] = new YYARec(317,-57 );yyac++; 
					yya[yyac] = new YYARec(318,-57 );yyac++; 
					yya[yyac] = new YYARec(319,-57 );yyac++; 
					yya[yyac] = new YYARec(320,-57 );yyac++; 
					yya[yyac] = new YYARec(321,-57 );yyac++; 
					yya[yyac] = new YYARec(322,-57 );yyac++; 
					yya[yyac] = new YYARec(324,-57 );yyac++; 
					yya[yyac] = new YYARec(0,-196 );yyac++; 
					yya[yyac] = new YYARec(260,-196 );yyac++; 
					yya[yyac] = new YYARec(261,-196 );yyac++; 
					yya[yyac] = new YYARec(297,-196 );yyac++; 
					yya[yyac] = new YYARec(298,-61 );yyac++; 
					yya[yyac] = new YYARec(299,-61 );yyac++; 
					yya[yyac] = new YYARec(300,-61 );yyac++; 
					yya[yyac] = new YYARec(301,-61 );yyac++; 
					yya[yyac] = new YYARec(302,-61 );yyac++; 
					yya[yyac] = new YYARec(303,-61 );yyac++; 
					yya[yyac] = new YYARec(304,-61 );yyac++; 
					yya[yyac] = new YYARec(305,-61 );yyac++; 
					yya[yyac] = new YYARec(306,-61 );yyac++; 
					yya[yyac] = new YYARec(307,-61 );yyac++; 
					yya[yyac] = new YYARec(309,-61 );yyac++; 
					yya[yyac] = new YYARec(310,-61 );yyac++; 
					yya[yyac] = new YYARec(311,-61 );yyac++; 
					yya[yyac] = new YYARec(312,-61 );yyac++; 
					yya[yyac] = new YYARec(313,-61 );yyac++; 
					yya[yyac] = new YYARec(314,-61 );yyac++; 
					yya[yyac] = new YYARec(315,-61 );yyac++; 
					yya[yyac] = new YYARec(316,-61 );yyac++; 
					yya[yyac] = new YYARec(317,-61 );yyac++; 
					yya[yyac] = new YYARec(318,-61 );yyac++; 
					yya[yyac] = new YYARec(319,-61 );yyac++; 
					yya[yyac] = new YYARec(320,-61 );yyac++; 
					yya[yyac] = new YYARec(321,-61 );yyac++; 
					yya[yyac] = new YYARec(324,-61 );yyac++; 
					yya[yyac] = new YYARec(0,-203 );yyac++; 
					yya[yyac] = new YYARec(260,-203 );yyac++; 
					yya[yyac] = new YYARec(261,-203 );yyac++; 
					yya[yyac] = new YYARec(297,-203 );yyac++; 
					yya[yyac] = new YYARec(298,-75 );yyac++; 
					yya[yyac] = new YYARec(299,-75 );yyac++; 
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
					yya[yyac] = new YYARec(311,-75 );yyac++; 
					yya[yyac] = new YYARec(312,-75 );yyac++; 
					yya[yyac] = new YYARec(313,-75 );yyac++; 
					yya[yyac] = new YYARec(314,-75 );yyac++; 
					yya[yyac] = new YYARec(315,-75 );yyac++; 
					yya[yyac] = new YYARec(316,-75 );yyac++; 
					yya[yyac] = new YYARec(317,-75 );yyac++; 
					yya[yyac] = new YYARec(318,-75 );yyac++; 
					yya[yyac] = new YYARec(319,-75 );yyac++; 
					yya[yyac] = new YYARec(320,-75 );yyac++; 
					yya[yyac] = new YYARec(321,-75 );yyac++; 
					yya[yyac] = new YYARec(324,-75 );yyac++; 
					yya[yyac] = new YYARec(0,-200 );yyac++; 
					yya[yyac] = new YYARec(260,-200 );yyac++; 
					yya[yyac] = new YYARec(261,-200 );yyac++; 
					yya[yyac] = new YYARec(297,-200 );yyac++; 
					yya[yyac] = new YYARec(263,-189 );yyac++; 
					yya[yyac] = new YYARec(282,-189 );yyac++; 
					yya[yyac] = new YYARec(283,-189 );yyac++; 
					yya[yyac] = new YYARec(287,-189 );yyac++; 
					yya[yyac] = new YYARec(322,-189 );yyac++; 
					yya[yyac] = new YYARec(323,-189 );yyac++; 
					yya[yyac] = new YYARec(324,-189 );yyac++; 
					yya[yyac] = new YYARec(325,-189 );yyac++; 
					yya[yyac] = new YYARec(326,-189 );yyac++; 
					yya[yyac] = new YYARec(0,-210 );yyac++; 
					yya[yyac] = new YYARec(260,-210 );yyac++; 
					yya[yyac] = new YYARec(261,-210 );yyac++; 
					yya[yyac] = new YYARec(297,-210 );yyac++; 
					yya[yyac] = new YYARec(263,-39 );yyac++; 
					yya[yyac] = new YYARec(282,-39 );yyac++; 
					yya[yyac] = new YYARec(283,-39 );yyac++; 
					yya[yyac] = new YYARec(287,-39 );yyac++; 
					yya[yyac] = new YYARec(322,-39 );yyac++; 
					yya[yyac] = new YYARec(323,-39 );yyac++; 
					yya[yyac] = new YYARec(324,-39 );yyac++; 
					yya[yyac] = new YYARec(325,-39 );yyac++; 
					yya[yyac] = new YYARec(326,-39 );yyac++; 
					yya[yyac] = new YYARec(0,-193 );yyac++; 
					yya[yyac] = new YYARec(260,-193 );yyac++; 
					yya[yyac] = new YYARec(261,-193 );yyac++; 
					yya[yyac] = new YYARec(297,-193 );yyac++; 
					yya[yyac] = new YYARec(263,-43 );yyac++; 
					yya[yyac] = new YYARec(298,-43 );yyac++; 
					yya[yyac] = new YYARec(299,-43 );yyac++; 
					yya[yyac] = new YYARec(300,-43 );yyac++; 
					yya[yyac] = new YYARec(301,-43 );yyac++; 
					yya[yyac] = new YYARec(302,-43 );yyac++; 
					yya[yyac] = new YYARec(303,-43 );yyac++; 
					yya[yyac] = new YYARec(304,-43 );yyac++; 
					yya[yyac] = new YYARec(305,-43 );yyac++; 
					yya[yyac] = new YYARec(306,-43 );yyac++; 
					yya[yyac] = new YYARec(307,-43 );yyac++; 
					yya[yyac] = new YYARec(309,-43 );yyac++; 
					yya[yyac] = new YYARec(310,-43 );yyac++; 
					yya[yyac] = new YYARec(311,-43 );yyac++; 
					yya[yyac] = new YYARec(312,-43 );yyac++; 
					yya[yyac] = new YYARec(313,-43 );yyac++; 
					yya[yyac] = new YYARec(314,-43 );yyac++; 
					yya[yyac] = new YYARec(315,-43 );yyac++; 
					yya[yyac] = new YYARec(316,-43 );yyac++; 
					yya[yyac] = new YYARec(317,-43 );yyac++; 
					yya[yyac] = new YYARec(318,-43 );yyac++; 
					yya[yyac] = new YYARec(319,-43 );yyac++; 
					yya[yyac] = new YYARec(320,-43 );yyac++; 
					yya[yyac] = new YYARec(321,-43 );yyac++; 
					yya[yyac] = new YYARec(324,-43 );yyac++; 
					yya[yyac] = new YYARec(0,-192 );yyac++; 
					yya[yyac] = new YYARec(260,-192 );yyac++; 
					yya[yyac] = new YYARec(261,-192 );yyac++; 
					yya[yyac] = new YYARec(297,-192 );yyac++; 
					yya[yyac] = new YYARec(298,-62 );yyac++; 
					yya[yyac] = new YYARec(299,-62 );yyac++; 
					yya[yyac] = new YYARec(300,-62 );yyac++; 
					yya[yyac] = new YYARec(301,-62 );yyac++; 
					yya[yyac] = new YYARec(302,-62 );yyac++; 
					yya[yyac] = new YYARec(303,-62 );yyac++; 
					yya[yyac] = new YYARec(304,-62 );yyac++; 
					yya[yyac] = new YYARec(305,-62 );yyac++; 
					yya[yyac] = new YYARec(306,-62 );yyac++; 
					yya[yyac] = new YYARec(307,-62 );yyac++; 
					yya[yyac] = new YYARec(309,-62 );yyac++; 
					yya[yyac] = new YYARec(310,-62 );yyac++; 
					yya[yyac] = new YYARec(311,-62 );yyac++; 
					yya[yyac] = new YYARec(312,-62 );yyac++; 
					yya[yyac] = new YYARec(313,-62 );yyac++; 
					yya[yyac] = new YYARec(314,-62 );yyac++; 
					yya[yyac] = new YYARec(315,-62 );yyac++; 
					yya[yyac] = new YYARec(316,-62 );yyac++; 
					yya[yyac] = new YYARec(317,-62 );yyac++; 
					yya[yyac] = new YYARec(318,-62 );yyac++; 
					yya[yyac] = new YYARec(319,-62 );yyac++; 
					yya[yyac] = new YYARec(320,-62 );yyac++; 
					yya[yyac] = new YYARec(321,-62 );yyac++; 
					yya[yyac] = new YYARec(324,-62 );yyac++; 
					yya[yyac] = new YYARec(0,-195 );yyac++; 
					yya[yyac] = new YYARec(260,-195 );yyac++; 
					yya[yyac] = new YYARec(261,-195 );yyac++; 
					yya[yyac] = new YYARec(297,-195 );yyac++; 
					yya[yyac] = new YYARec(263,-187 );yyac++; 
					yya[yyac] = new YYARec(282,-187 );yyac++; 
					yya[yyac] = new YYARec(283,-187 );yyac++; 
					yya[yyac] = new YYARec(287,-187 );yyac++; 
					yya[yyac] = new YYARec(322,-187 );yyac++; 
					yya[yyac] = new YYARec(323,-187 );yyac++; 
					yya[yyac] = new YYARec(324,-187 );yyac++; 
					yya[yyac] = new YYARec(325,-187 );yyac++; 
					yya[yyac] = new YYARec(326,-187 );yyac++; 
					yya[yyac] = new YYARec(0,-206 );yyac++; 
					yya[yyac] = new YYARec(260,-206 );yyac++; 
					yya[yyac] = new YYARec(261,-206 );yyac++; 
					yya[yyac] = new YYARec(297,-206 );yyac++; 
					yya[yyac] = new YYARec(263,-188 );yyac++; 
					yya[yyac] = new YYARec(282,-188 );yyac++; 
					yya[yyac] = new YYARec(283,-188 );yyac++; 
					yya[yyac] = new YYARec(287,-188 );yyac++; 
					yya[yyac] = new YYARec(322,-188 );yyac++; 
					yya[yyac] = new YYARec(323,-188 );yyac++; 
					yya[yyac] = new YYARec(324,-188 );yyac++; 
					yya[yyac] = new YYARec(325,-188 );yyac++; 
					yya[yyac] = new YYARec(326,-188 );yyac++; 
					yya[yyac] = new YYARec(0,-208 );yyac++; 
					yya[yyac] = new YYARec(260,-208 );yyac++; 
					yya[yyac] = new YYARec(261,-208 );yyac++; 
					yya[yyac] = new YYARec(297,-208 );yyac++; 
					yya[yyac] = new YYARec(263,-40 );yyac++; 
					yya[yyac] = new YYARec(282,-40 );yyac++; 
					yya[yyac] = new YYARec(283,-40 );yyac++; 
					yya[yyac] = new YYARec(287,-40 );yyac++; 
					yya[yyac] = new YYARec(322,-40 );yyac++; 
					yya[yyac] = new YYARec(323,-40 );yyac++; 
					yya[yyac] = new YYARec(324,-40 );yyac++; 
					yya[yyac] = new YYARec(325,-40 );yyac++; 
					yya[yyac] = new YYARec(326,-40 );yyac++; 
					yya[yyac] = new YYARec(0,-205 );yyac++; 
					yya[yyac] = new YYARec(260,-205 );yyac++; 
					yya[yyac] = new YYARec(261,-205 );yyac++; 
					yya[yyac] = new YYARec(297,-205 );yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(305,96);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(313,100);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(316,102);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(318,104);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(321,107);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(258,110);yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,128);yyac++; 
					yya[yyac] = new YYARec(258,129);yyac++; 
					yya[yyac] = new YYARec(258,130);yyac++; 
					yya[yyac] = new YYARec(263,131);yyac++; 
					yya[yyac] = new YYARec(258,132);yyac++; 
					yya[yyac] = new YYARec(258,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(266,136);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(322,138);yyac++; 
					yya[yyac] = new YYARec(323,139);yyac++; 
					yya[yyac] = new YYARec(258,140);yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(258,179);yyac++; 
					yya[yyac] = new YYARec(257,184);yyac++; 
					yya[yyac] = new YYARec(259,185);yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-45 );yyac++; 
					yya[yyac] = new YYARec(258,188);yyac++; 
					yya[yyac] = new YYARec(260,189);yyac++; 
					yya[yyac] = new YYARec(261,190);yyac++; 
					yya[yyac] = new YYARec(258,191);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,138);yyac++; 
					yya[yyac] = new YYARec(323,139);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,193);yyac++; 
					yya[yyac] = new YYARec(297,54);yyac++; 
					yya[yyac] = new YYARec(289,-154 );yyac++; 
					yya[yyac] = new YYARec(290,-154 );yyac++; 
					yya[yyac] = new YYARec(291,-154 );yyac++; 
					yya[yyac] = new YYARec(292,-154 );yyac++; 
					yya[yyac] = new YYARec(293,-154 );yyac++; 
					yya[yyac] = new YYARec(268,-220 );yyac++; 
					yya[yyac] = new YYARec(258,194);yyac++; 
					yya[yyac] = new YYARec(258,196);yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(308,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(268,199);yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(308,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(268,-213 );yyac++; 
					yya[yyac] = new YYARec(289,-213 );yyac++; 
					yya[yyac] = new YYARec(290,-213 );yyac++; 
					yya[yyac] = new YYARec(291,-213 );yyac++; 
					yya[yyac] = new YYARec(292,-213 );yyac++; 
					yya[yyac] = new YYARec(293,-213 );yyac++; 
					yya[yyac] = new YYARec(297,-213 );yyac++; 
					yya[yyac] = new YYARec(267,201);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(289,204);yyac++; 
					yya[yyac] = new YYARec(290,205);yyac++; 
					yya[yyac] = new YYARec(291,206);yyac++; 
					yya[yyac] = new YYARec(292,207);yyac++; 
					yya[yyac] = new YYARec(293,208);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(266,235);yyac++; 
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
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-176 );yyac++; 
					yya[yyac] = new YYARec(263,-176 );yyac++; 
					yya[yyac] = new YYARec(282,-176 );yyac++; 
					yya[yyac] = new YYARec(283,-176 );yyac++; 
					yya[yyac] = new YYARec(287,-176 );yyac++; 
					yya[yyac] = new YYARec(298,-176 );yyac++; 
					yya[yyac] = new YYARec(299,-176 );yyac++; 
					yya[yyac] = new YYARec(300,-176 );yyac++; 
					yya[yyac] = new YYARec(301,-176 );yyac++; 
					yya[yyac] = new YYARec(302,-176 );yyac++; 
					yya[yyac] = new YYARec(303,-176 );yyac++; 
					yya[yyac] = new YYARec(304,-176 );yyac++; 
					yya[yyac] = new YYARec(305,-176 );yyac++; 
					yya[yyac] = new YYARec(306,-176 );yyac++; 
					yya[yyac] = new YYARec(307,-176 );yyac++; 
					yya[yyac] = new YYARec(308,-176 );yyac++; 
					yya[yyac] = new YYARec(309,-176 );yyac++; 
					yya[yyac] = new YYARec(310,-176 );yyac++; 
					yya[yyac] = new YYARec(311,-176 );yyac++; 
					yya[yyac] = new YYARec(312,-176 );yyac++; 
					yya[yyac] = new YYARec(313,-176 );yyac++; 
					yya[yyac] = new YYARec(314,-176 );yyac++; 
					yya[yyac] = new YYARec(315,-176 );yyac++; 
					yya[yyac] = new YYARec(316,-176 );yyac++; 
					yya[yyac] = new YYARec(317,-176 );yyac++; 
					yya[yyac] = new YYARec(318,-176 );yyac++; 
					yya[yyac] = new YYARec(319,-176 );yyac++; 
					yya[yyac] = new YYARec(320,-176 );yyac++; 
					yya[yyac] = new YYARec(321,-176 );yyac++; 
					yya[yyac] = new YYARec(322,-176 );yyac++; 
					yya[yyac] = new YYARec(323,-176 );yyac++; 
					yya[yyac] = new YYARec(324,-176 );yyac++; 
					yya[yyac] = new YYARec(325,-176 );yyac++; 
					yya[yyac] = new YYARec(326,-176 );yyac++; 
					yya[yyac] = new YYARec(268,-197 );yyac++; 
					yya[yyac] = new YYARec(289,-197 );yyac++; 
					yya[yyac] = new YYARec(290,-197 );yyac++; 
					yya[yyac] = new YYARec(291,-197 );yyac++; 
					yya[yyac] = new YYARec(292,-197 );yyac++; 
					yya[yyac] = new YYARec(293,-197 );yyac++; 
					yya[yyac] = new YYARec(297,-197 );yyac++; 
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
					yya[yyac] = new YYARec(303,-175 );yyac++; 
					yya[yyac] = new YYARec(304,-175 );yyac++; 
					yya[yyac] = new YYARec(305,-175 );yyac++; 
					yya[yyac] = new YYARec(306,-175 );yyac++; 
					yya[yyac] = new YYARec(307,-175 );yyac++; 
					yya[yyac] = new YYARec(308,-175 );yyac++; 
					yya[yyac] = new YYARec(309,-175 );yyac++; 
					yya[yyac] = new YYARec(310,-175 );yyac++; 
					yya[yyac] = new YYARec(311,-175 );yyac++; 
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
					yya[yyac] = new YYARec(326,-175 );yyac++; 
					yya[yyac] = new YYARec(268,-194 );yyac++; 
					yya[yyac] = new YYARec(289,-194 );yyac++; 
					yya[yyac] = new YYARec(290,-194 );yyac++; 
					yya[yyac] = new YYARec(291,-194 );yyac++; 
					yya[yyac] = new YYARec(292,-194 );yyac++; 
					yya[yyac] = new YYARec(293,-194 );yyac++; 
					yya[yyac] = new YYARec(297,-194 );yyac++; 
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
					yya[yyac] = new YYARec(268,-190 );yyac++; 
					yya[yyac] = new YYARec(289,-190 );yyac++; 
					yya[yyac] = new YYARec(290,-190 );yyac++; 
					yya[yyac] = new YYARec(291,-190 );yyac++; 
					yya[yyac] = new YYARec(292,-190 );yyac++; 
					yya[yyac] = new YYARec(293,-190 );yyac++; 
					yya[yyac] = new YYARec(297,-190 );yyac++; 
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
					yya[yyac] = new YYARec(303,-174 );yyac++; 
					yya[yyac] = new YYARec(304,-174 );yyac++; 
					yya[yyac] = new YYARec(305,-174 );yyac++; 
					yya[yyac] = new YYARec(306,-174 );yyac++; 
					yya[yyac] = new YYARec(307,-174 );yyac++; 
					yya[yyac] = new YYARec(308,-174 );yyac++; 
					yya[yyac] = new YYARec(309,-174 );yyac++; 
					yya[yyac] = new YYARec(310,-174 );yyac++; 
					yya[yyac] = new YYARec(311,-174 );yyac++; 
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
					yya[yyac] = new YYARec(326,-174 );yyac++; 
					yya[yyac] = new YYARec(268,-191 );yyac++; 
					yya[yyac] = new YYARec(289,-191 );yyac++; 
					yya[yyac] = new YYARec(290,-191 );yyac++; 
					yya[yyac] = new YYARec(291,-191 );yyac++; 
					yya[yyac] = new YYARec(292,-191 );yyac++; 
					yya[yyac] = new YYARec(293,-191 );yyac++; 
					yya[yyac] = new YYARec(297,-191 );yyac++; 
					yya[yyac] = new YYARec(258,239);yyac++; 
					yya[yyac] = new YYARec(263,-185 );yyac++; 
					yya[yyac] = new YYARec(268,-185 );yyac++; 
					yya[yyac] = new YYARec(282,-185 );yyac++; 
					yya[yyac] = new YYARec(283,-185 );yyac++; 
					yya[yyac] = new YYARec(287,-185 );yyac++; 
					yya[yyac] = new YYARec(289,-185 );yyac++; 
					yya[yyac] = new YYARec(290,-185 );yyac++; 
					yya[yyac] = new YYARec(291,-185 );yyac++; 
					yya[yyac] = new YYARec(292,-185 );yyac++; 
					yya[yyac] = new YYARec(293,-185 );yyac++; 
					yya[yyac] = new YYARec(297,-185 );yyac++; 
					yya[yyac] = new YYARec(298,-185 );yyac++; 
					yya[yyac] = new YYARec(299,-185 );yyac++; 
					yya[yyac] = new YYARec(300,-185 );yyac++; 
					yya[yyac] = new YYARec(301,-185 );yyac++; 
					yya[yyac] = new YYARec(302,-185 );yyac++; 
					yya[yyac] = new YYARec(303,-185 );yyac++; 
					yya[yyac] = new YYARec(304,-185 );yyac++; 
					yya[yyac] = new YYARec(305,-185 );yyac++; 
					yya[yyac] = new YYARec(306,-185 );yyac++; 
					yya[yyac] = new YYARec(307,-185 );yyac++; 
					yya[yyac] = new YYARec(308,-185 );yyac++; 
					yya[yyac] = new YYARec(309,-185 );yyac++; 
					yya[yyac] = new YYARec(310,-185 );yyac++; 
					yya[yyac] = new YYARec(311,-185 );yyac++; 
					yya[yyac] = new YYARec(312,-185 );yyac++; 
					yya[yyac] = new YYARec(313,-185 );yyac++; 
					yya[yyac] = new YYARec(314,-185 );yyac++; 
					yya[yyac] = new YYARec(315,-185 );yyac++; 
					yya[yyac] = new YYARec(316,-185 );yyac++; 
					yya[yyac] = new YYARec(317,-185 );yyac++; 
					yya[yyac] = new YYARec(318,-185 );yyac++; 
					yya[yyac] = new YYARec(319,-185 );yyac++; 
					yya[yyac] = new YYARec(320,-185 );yyac++; 
					yya[yyac] = new YYARec(321,-185 );yyac++; 
					yya[yyac] = new YYARec(322,-185 );yyac++; 
					yya[yyac] = new YYARec(323,-185 );yyac++; 
					yya[yyac] = new YYARec(324,-185 );yyac++; 
					yya[yyac] = new YYARec(325,-185 );yyac++; 
					yya[yyac] = new YYARec(326,-185 );yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(258,241);yyac++; 
					yya[yyac] = new YYARec(267,242);yyac++; 
					yya[yyac] = new YYARec(257,184);yyac++; 
					yya[yyac] = new YYARec(259,185);yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(309,81);yyac++; 
					yya[yyac] = new YYARec(315,82);yyac++; 
					yya[yyac] = new YYARec(317,83);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(258,251);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,265);yyac++; 
					yya[yyac] = new YYARec(274,266);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(284,269);yyac++; 
					yya[yyac] = new YYARec(285,270);yyac++; 
					yya[yyac] = new YYARec(286,271);yyac++; 
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
					yya[yyac] = new YYARec(282,273);yyac++; 
					yya[yyac] = new YYARec(283,274);yyac++; 
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
					yya[yyac] = new YYARec(278,276);yyac++; 
					yya[yyac] = new YYARec(279,277);yyac++; 
					yya[yyac] = new YYARec(280,278);yyac++; 
					yya[yyac] = new YYARec(281,279);yyac++; 
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
					yya[yyac] = new YYARec(276,281);yyac++; 
					yya[yyac] = new YYARec(277,282);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(266,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(273,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(273,283);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(272,284);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(271,285);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(270,286);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(269,287);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(289,204);yyac++; 
					yya[yyac] = new YYARec(290,205);yyac++; 
					yya[yyac] = new YYARec(291,206);yyac++; 
					yya[yyac] = new YYARec(292,207);yyac++; 
					yya[yyac] = new YYARec(293,208);yyac++; 
					yya[yyac] = new YYARec(258,-123 );yyac++; 
					yya[yyac] = new YYARec(269,-123 );yyac++; 
					yya[yyac] = new YYARec(270,-123 );yyac++; 
					yya[yyac] = new YYARec(271,-123 );yyac++; 
					yya[yyac] = new YYARec(272,-123 );yyac++; 
					yya[yyac] = new YYARec(273,-123 );yyac++; 
					yya[yyac] = new YYARec(276,-123 );yyac++; 
					yya[yyac] = new YYARec(277,-123 );yyac++; 
					yya[yyac] = new YYARec(278,-123 );yyac++; 
					yya[yyac] = new YYARec(279,-123 );yyac++; 
					yya[yyac] = new YYARec(280,-123 );yyac++; 
					yya[yyac] = new YYARec(281,-123 );yyac++; 
					yya[yyac] = new YYARec(282,-123 );yyac++; 
					yya[yyac] = new YYARec(283,-123 );yyac++; 
					yya[yyac] = new YYARec(284,-123 );yyac++; 
					yya[yyac] = new YYARec(285,-123 );yyac++; 
					yya[yyac] = new YYARec(286,-123 );yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,-148 );yyac++; 
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
					yya[yyac] = new YYARec(274,-146 );yyac++; 
					yya[yyac] = new YYARec(258,-194 );yyac++; 
					yya[yyac] = new YYARec(266,-194 );yyac++; 
					yya[yyac] = new YYARec(269,-194 );yyac++; 
					yya[yyac] = new YYARec(270,-194 );yyac++; 
					yya[yyac] = new YYARec(271,-194 );yyac++; 
					yya[yyac] = new YYARec(272,-194 );yyac++; 
					yya[yyac] = new YYARec(273,-194 );yyac++; 
					yya[yyac] = new YYARec(275,-194 );yyac++; 
					yya[yyac] = new YYARec(276,-194 );yyac++; 
					yya[yyac] = new YYARec(277,-194 );yyac++; 
					yya[yyac] = new YYARec(278,-194 );yyac++; 
					yya[yyac] = new YYARec(279,-194 );yyac++; 
					yya[yyac] = new YYARec(280,-194 );yyac++; 
					yya[yyac] = new YYARec(281,-194 );yyac++; 
					yya[yyac] = new YYARec(282,-194 );yyac++; 
					yya[yyac] = new YYARec(283,-194 );yyac++; 
					yya[yyac] = new YYARec(284,-194 );yyac++; 
					yya[yyac] = new YYARec(285,-194 );yyac++; 
					yya[yyac] = new YYARec(286,-194 );yyac++; 
					yya[yyac] = new YYARec(289,-194 );yyac++; 
					yya[yyac] = new YYARec(290,-194 );yyac++; 
					yya[yyac] = new YYARec(291,-194 );yyac++; 
					yya[yyac] = new YYARec(292,-194 );yyac++; 
					yya[yyac] = new YYARec(293,-194 );yyac++; 
					yya[yyac] = new YYARec(297,-194 );yyac++; 
					yya[yyac] = new YYARec(274,-147 );yyac++; 
					yya[yyac] = new YYARec(258,-206 );yyac++; 
					yya[yyac] = new YYARec(266,-206 );yyac++; 
					yya[yyac] = new YYARec(269,-206 );yyac++; 
					yya[yyac] = new YYARec(270,-206 );yyac++; 
					yya[yyac] = new YYARec(271,-206 );yyac++; 
					yya[yyac] = new YYARec(272,-206 );yyac++; 
					yya[yyac] = new YYARec(273,-206 );yyac++; 
					yya[yyac] = new YYARec(275,-206 );yyac++; 
					yya[yyac] = new YYARec(276,-206 );yyac++; 
					yya[yyac] = new YYARec(277,-206 );yyac++; 
					yya[yyac] = new YYARec(278,-206 );yyac++; 
					yya[yyac] = new YYARec(279,-206 );yyac++; 
					yya[yyac] = new YYARec(280,-206 );yyac++; 
					yya[yyac] = new YYARec(281,-206 );yyac++; 
					yya[yyac] = new YYARec(282,-206 );yyac++; 
					yya[yyac] = new YYARec(283,-206 );yyac++; 
					yya[yyac] = new YYARec(284,-206 );yyac++; 
					yya[yyac] = new YYARec(285,-206 );yyac++; 
					yya[yyac] = new YYARec(286,-206 );yyac++; 
					yya[yyac] = new YYARec(289,-206 );yyac++; 
					yya[yyac] = new YYARec(290,-206 );yyac++; 
					yya[yyac] = new YYARec(291,-206 );yyac++; 
					yya[yyac] = new YYARec(292,-206 );yyac++; 
					yya[yyac] = new YYARec(293,-206 );yyac++; 
					yya[yyac] = new YYARec(297,-206 );yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(266,291);yyac++; 
					yya[yyac] = new YYARec(266,292);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(257,184);yyac++; 
					yya[yyac] = new YYARec(259,185);yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(258,301);yyac++; 
					yya[yyac] = new YYARec(258,302);yyac++; 
					yya[yyac] = new YYARec(322,138);yyac++; 
					yya[yyac] = new YYARec(263,303);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(258,304);yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(265,28);yyac++; 
					yya[yyac] = new YYARec(267,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,40);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,42);yyac++; 
					yya[yyac] = new YYARec(312,43);yyac++; 
					yya[yyac] = new YYARec(313,44);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,46);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,48);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(308,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(258,307);yyac++; 
					yya[yyac] = new YYARec(258,308);yyac++; 
					yya[yyac] = new YYARec(260,309);yyac++; 
					yya[yyac] = new YYARec(261,310);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(260,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(274,229);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,230);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,231);yyac++; 
					yya[yyac] = new YYARec(315,232);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,233);yyac++; 
					yya[yyac] = new YYARec(323,234);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(275,323);yyac++; 
					yya[yyac] = new YYARec(267,324);yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(263,75);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(311,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(326,-97 );yyac++; 
					yya[yyac] = new YYARec(257,184);yyac++; 
					yya[yyac] = new YYARec(259,185);yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(257,184);yyac++; 
					yya[yyac] = new YYARec(259,185);yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(261,332);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,154);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(258,-89 );yyac++; 
					yya[yyac] = new YYARec(257,166);yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(259,168);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(264,27);yyac++; 
					yya[yyac] = new YYARec(266,169);yyac++; 
					yya[yyac] = new YYARec(288,170);yyac++; 
					yya[yyac] = new YYARec(294,171);yyac++; 
					yya[yyac] = new YYARec(295,172);yyac++; 
					yya[yyac] = new YYARec(296,173);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,174);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,175);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,176);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,177);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(324,178);yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(275,335);yyac++; 
					yya[yyac] = new YYARec(284,269);yyac++; 
					yya[yyac] = new YYARec(285,270);yyac++; 
					yya[yyac] = new YYARec(286,271);yyac++; 
					yya[yyac] = new YYARec(258,-114 );yyac++; 
					yya[yyac] = new YYARec(266,-114 );yyac++; 
					yya[yyac] = new YYARec(269,-114 );yyac++; 
					yya[yyac] = new YYARec(270,-114 );yyac++; 
					yya[yyac] = new YYARec(271,-114 );yyac++; 
					yya[yyac] = new YYARec(272,-114 );yyac++; 
					yya[yyac] = new YYARec(273,-114 );yyac++; 
					yya[yyac] = new YYARec(275,-114 );yyac++; 
					yya[yyac] = new YYARec(276,-114 );yyac++; 
					yya[yyac] = new YYARec(277,-114 );yyac++; 
					yya[yyac] = new YYARec(278,-114 );yyac++; 
					yya[yyac] = new YYARec(279,-114 );yyac++; 
					yya[yyac] = new YYARec(280,-114 );yyac++; 
					yya[yyac] = new YYARec(281,-114 );yyac++; 
					yya[yyac] = new YYARec(282,-114 );yyac++; 
					yya[yyac] = new YYARec(283,-114 );yyac++; 
					yya[yyac] = new YYARec(282,273);yyac++; 
					yya[yyac] = new YYARec(283,274);yyac++; 
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
					yya[yyac] = new YYARec(278,276);yyac++; 
					yya[yyac] = new YYARec(279,277);yyac++; 
					yya[yyac] = new YYARec(280,278);yyac++; 
					yya[yyac] = new YYARec(281,279);yyac++; 
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
					yya[yyac] = new YYARec(276,281);yyac++; 
					yya[yyac] = new YYARec(277,282);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(273,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(273,283);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(272,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(272,284);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(271,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(271,285);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(270,286);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(267,336);yyac++; 
					yya[yyac] = new YYARec(267,337);yyac++; 
					yya[yyac] = new YYARec(282,120);yyac++; 
					yya[yyac] = new YYARec(283,121);yyac++; 
					yya[yyac] = new YYARec(287,122);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,57);yyac++; 
					yya[yyac] = new YYARec(300,58);yyac++; 
					yya[yyac] = new YYARec(301,59);yyac++; 
					yya[yyac] = new YYARec(302,60);yyac++; 
					yya[yyac] = new YYARec(303,61);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(309,62);yyac++; 
					yya[yyac] = new YYARec(310,41);yyac++; 
					yya[yyac] = new YYARec(311,63);yyac++; 
					yya[yyac] = new YYARec(312,64);yyac++; 
					yya[yyac] = new YYARec(313,65);yyac++; 
					yya[yyac] = new YYARec(314,45);yyac++; 
					yya[yyac] = new YYARec(315,66);yyac++; 
					yya[yyac] = new YYARec(316,47);yyac++; 
					yya[yyac] = new YYARec(317,67);yyac++; 
					yya[yyac] = new YYARec(318,49);yyac++; 
					yya[yyac] = new YYARec(319,68);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,52);yyac++; 
					yya[yyac] = new YYARec(322,123);yyac++; 
					yya[yyac] = new YYARec(323,124);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(325,88);yyac++; 
					yya[yyac] = new YYARec(326,125);yyac++; 
					yya[yyac] = new YYARec(258,-68 );yyac++; 
					yya[yyac] = new YYARec(260,339);yyac++; 
					yya[yyac] = new YYARec(261,340);yyac++; 
					yya[yyac] = new YYARec(258,341);yyac++; 
					yya[yyac] = new YYARec(258,342);yyac++; 
					yya[yyac] = new YYARec(261,343);yyac++; 
					yya[yyac] = new YYARec(258,344);yyac++; 
					yya[yyac] = new YYARec(257,184);yyac++; 
					yya[yyac] = new YYARec(259,185);yyac++; 
					yya[yyac] = new YYARec(301,94);yyac++; 
					yya[yyac] = new YYARec(302,95);yyac++; 
					yya[yyac] = new YYARec(306,97);yyac++; 
					yya[yyac] = new YYARec(311,98);yyac++; 
					yya[yyac] = new YYARec(312,99);yyac++; 
					yya[yyac] = new YYARec(315,101);yyac++; 
					yya[yyac] = new YYARec(317,103);yyac++; 
					yya[yyac] = new YYARec(319,105);yyac++; 
					yya[yyac] = new YYARec(320,106);yyac++; 
					yya[yyac] = new YYARec(324,53);yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(261,346);yyac++;

					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,21);yygc++; 
					yyg[yygc] = new YYARec(-2,22);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,55);yygc++; 
					yyg[yygc] = new YYARec(-47,56);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,69);yygc++; 
					yyg[yygc] = new YYARec(-34,70);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,71);yygc++; 
					yyg[yygc] = new YYARec(-36,72);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-30,74);yygc++; 
					yyg[yygc] = new YYARec(-30,76);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,77);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,80);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,84);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,85);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,86);yygc++; 
					yyg[yygc] = new YYARec(-26,87);yygc++; 
					yyg[yygc] = new YYARec(-76,89);yygc++; 
					yyg[yygc] = new YYARec(-43,90);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-30,108);yygc++; 
					yyg[yygc] = new YYARec(-30,109);yygc++; 
					yyg[yygc] = new YYARec(-30,111);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-71,113);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-33,115);yygc++; 
					yyg[yygc] = new YYARec(-27,116);yygc++; 
					yyg[yygc] = new YYARec(-26,117);yygc++; 
					yyg[yygc] = new YYARec(-24,118);yygc++; 
					yyg[yygc] = new YYARec(-23,119);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,69);yygc++; 
					yyg[yygc] = new YYARec(-34,126);yygc++; 
					yyg[yygc] = new YYARec(-31,127);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-24,135);yygc++; 
					yyg[yygc] = new YYARec(-26,137);yygc++; 
					yyg[yygc] = new YYARec(-30,141);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-14,143);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,144);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-14,145);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,144);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,146);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-28,147);yygc++; 
					yyg[yygc] = new YYARec(-27,148);yygc++; 
					yyg[yygc] = new YYARec(-26,149);yygc++; 
					yyg[yygc] = new YYARec(-25,150);yygc++; 
					yyg[yygc] = new YYARec(-24,151);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-21,152);yygc++; 
					yyg[yygc] = new YYARec(-5,153);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,162);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-43,180);yygc++; 
					yyg[yygc] = new YYARec(-42,181);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-20,182);yygc++; 
					yyg[yygc] = new YYARec(-18,183);yygc++; 
					yyg[yygc] = new YYARec(-30,186);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,69);yygc++; 
					yyg[yygc] = new YYARec(-34,126);yygc++; 
					yyg[yygc] = new YYARec(-31,187);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,192);yygc++; 
					yyg[yygc] = new YYARec(-30,195);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,197);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,198);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-30,200);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,202);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-74,203);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,209);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,210);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,211);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,226);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,228);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,236);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,238);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-30,240);yygc++; 
					yyg[yygc] = new YYARec(-43,180);yygc++; 
					yyg[yygc] = new YYARec(-42,181);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-20,243);yygc++; 
					yyg[yygc] = new YYARec(-18,183);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,244);yygc++; 
					yyg[yygc] = new YYARec(-23,78);yygc++; 
					yyg[yygc] = new YYARec(-22,79);yygc++; 
					yyg[yygc] = new YYARec(-13,245);yygc++; 
					yyg[yygc] = new YYARec(-71,246);yygc++; 
					yyg[yygc] = new YYARec(-39,247);yygc++; 
					yyg[yygc] = new YYARec(-38,248);yygc++; 
					yyg[yygc] = new YYARec(-37,249);yygc++; 
					yyg[yygc] = new YYARec(-23,250);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,146);yygc++; 
					yyg[yygc] = new YYARec(-54,252);yygc++; 
					yyg[yygc] = new YYARec(-52,253);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-28,254);yygc++; 
					yyg[yygc] = new YYARec(-27,255);yygc++; 
					yyg[yygc] = new YYARec(-26,256);yygc++; 
					yyg[yygc] = new YYARec(-25,257);yygc++; 
					yyg[yygc] = new YYARec(-24,258);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,153);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,259);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,146);yygc++; 
					yyg[yygc] = new YYARec(-54,252);yygc++; 
					yyg[yygc] = new YYARec(-52,260);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-28,254);yygc++; 
					yyg[yygc] = new YYARec(-27,255);yygc++; 
					yyg[yygc] = new YYARec(-26,256);yygc++; 
					yyg[yygc] = new YYARec(-25,257);yygc++; 
					yyg[yygc] = new YYARec(-24,258);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,153);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,261);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,262);yygc++; 
					yyg[yygc] = new YYARec(-16,263);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,262);yygc++; 
					yyg[yygc] = new YYARec(-16,264);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,267);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-69,268);yygc++; 
					yyg[yygc] = new YYARec(-67,272);yygc++; 
					yyg[yygc] = new YYARec(-65,275);yygc++; 
					yyg[yygc] = new YYARec(-63,280);yygc++; 
					yyg[yygc] = new YYARec(-74,288);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,289);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,290);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,293);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,146);yygc++; 
					yyg[yygc] = new YYARec(-45,294);yygc++; 
					yyg[yygc] = new YYARec(-44,295);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-28,296);yygc++; 
					yyg[yygc] = new YYARec(-27,297);yygc++; 
					yyg[yygc] = new YYARec(-26,298);yygc++; 
					yyg[yygc] = new YYARec(-24,299);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,153);yygc++; 
					yyg[yygc] = new YYARec(-43,180);yygc++; 
					yyg[yygc] = new YYARec(-42,181);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-20,300);yygc++; 
					yyg[yygc] = new YYARec(-18,183);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-46,4);yygc++; 
					yyg[yygc] = new YYARec(-41,5);yygc++; 
					yyg[yygc] = new YYARec(-40,6);yygc++; 
					yyg[yygc] = new YYARec(-35,7);yygc++; 
					yyg[yygc] = new YYARec(-32,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,305);yygc++; 
					yyg[yygc] = new YYARec(-30,306);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,311);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,312);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,313);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,314);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,315);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,316);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,317);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,318);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,319);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,320);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,321);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-73,212);yygc++; 
					yyg[yygc] = new YYARec(-72,213);yygc++; 
					yyg[yygc] = new YYARec(-71,214);yygc++; 
					yyg[yygc] = new YYARec(-70,215);yygc++; 
					yyg[yygc] = new YYARec(-68,216);yygc++; 
					yyg[yygc] = new YYARec(-66,217);yygc++; 
					yyg[yygc] = new YYARec(-64,218);yygc++; 
					yyg[yygc] = new YYARec(-62,219);yygc++; 
					yyg[yygc] = new YYARec(-61,220);yygc++; 
					yyg[yygc] = new YYARec(-60,221);yygc++; 
					yyg[yygc] = new YYARec(-59,222);yygc++; 
					yyg[yygc] = new YYARec(-58,223);yygc++; 
					yyg[yygc] = new YYARec(-57,224);yygc++; 
					yyg[yygc] = new YYARec(-56,225);yygc++; 
					yyg[yygc] = new YYARec(-55,322);yygc++; 
					yyg[yygc] = new YYARec(-25,227);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,237);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,325);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,326);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-30,327);yygc++; 
					yyg[yygc] = new YYARec(-43,180);yygc++; 
					yyg[yygc] = new YYARec(-42,181);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-20,328);yygc++; 
					yyg[yygc] = new YYARec(-19,329);yygc++; 
					yyg[yygc] = new YYARec(-18,183);yygc++; 
					yyg[yygc] = new YYARec(-43,180);yygc++; 
					yyg[yygc] = new YYARec(-42,181);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-20,328);yygc++; 
					yyg[yygc] = new YYARec(-19,330);yygc++; 
					yyg[yygc] = new YYARec(-18,183);yygc++; 
					yyg[yygc] = new YYARec(-71,246);yygc++; 
					yyg[yygc] = new YYARec(-39,247);yygc++; 
					yyg[yygc] = new YYARec(-38,248);yygc++; 
					yyg[yygc] = new YYARec(-37,331);yygc++; 
					yyg[yygc] = new YYARec(-23,250);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,146);yygc++; 
					yyg[yygc] = new YYARec(-54,252);yygc++; 
					yyg[yygc] = new YYARec(-52,333);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-28,254);yygc++; 
					yyg[yygc] = new YYARec(-27,255);yygc++; 
					yyg[yygc] = new YYARec(-26,256);yygc++; 
					yyg[yygc] = new YYARec(-25,257);yygc++; 
					yyg[yygc] = new YYARec(-24,258);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,153);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-75,155);yygc++; 
					yyg[yygc] = new YYARec(-53,156);yygc++; 
					yyg[yygc] = new YYARec(-51,157);yygc++; 
					yyg[yygc] = new YYARec(-50,158);yygc++; 
					yyg[yygc] = new YYARec(-49,159);yygc++; 
					yyg[yygc] = new YYARec(-48,160);yygc++; 
					yyg[yygc] = new YYARec(-23,161);yygc++; 
					yyg[yygc] = new YYARec(-17,334);yygc++; 
					yyg[yygc] = new YYARec(-15,163);yygc++; 
					yyg[yygc] = new YYARec(-11,164);yygc++; 
					yyg[yygc] = new YYARec(-5,165);yygc++; 
					yyg[yygc] = new YYARec(-69,268);yygc++; 
					yyg[yygc] = new YYARec(-67,272);yygc++; 
					yyg[yygc] = new YYARec(-65,275);yygc++; 
					yyg[yygc] = new YYARec(-63,280);yygc++; 
					yyg[yygc] = new YYARec(-79,1);yygc++; 
					yyg[yygc] = new YYARec(-78,2);yygc++; 
					yyg[yygc] = new YYARec(-77,112);yygc++; 
					yyg[yygc] = new YYARec(-75,3);yygc++; 
					yyg[yygc] = new YYARec(-71,146);yygc++; 
					yyg[yygc] = new YYARec(-45,294);yygc++; 
					yyg[yygc] = new YYARec(-44,338);yygc++; 
					yyg[yygc] = new YYARec(-39,114);yygc++; 
					yyg[yygc] = new YYARec(-28,296);yygc++; 
					yyg[yygc] = new YYARec(-27,297);yygc++; 
					yyg[yygc] = new YYARec(-26,298);yygc++; 
					yyg[yygc] = new YYARec(-24,299);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,153);yygc++; 
					yyg[yygc] = new YYARec(-43,180);yygc++; 
					yyg[yygc] = new YYARec(-42,181);yygc++; 
					yyg[yygc] = new YYARec(-41,91);yygc++; 
					yyg[yygc] = new YYARec(-35,92);yygc++; 
					yyg[yygc] = new YYARec(-23,93);yygc++; 
					yyg[yygc] = new YYARec(-20,345);yygc++; 
					yyg[yygc] = new YYARec(-18,183);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -215;  
					yyd[2] = -214;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = -63;  
					yyd[6] = 0;  
					yyd[7] = 0;  
					yyd[8] = 0;  
					yyd[9] = 0;  
					yyd[10] = -213;  
					yyd[11] = -42;  
					yyd[12] = -13;  
					yyd[13] = -12;  
					yyd[14] = -11;  
					yyd[15] = -10;  
					yyd[16] = -9;  
					yyd[17] = -8;  
					yyd[18] = -7;  
					yyd[19] = -3;  
					yyd[20] = 0;  
					yyd[21] = -1;  
					yyd[22] = 0;  
					yyd[23] = 0;  
					yyd[24] = -5;  
					yyd[25] = 0;  
					yyd[26] = 0;  
					yyd[27] = 0;  
					yyd[28] = 0;  
					yyd[29] = -6;  
					yyd[30] = -212;  
					yyd[31] = 0;  
					yyd[32] = 0;  
					yyd[33] = 0;  
					yyd[34] = 0;  
					yyd[35] = 0;  
					yyd[36] = -202;  
					yyd[37] = -199;  
					yyd[38] = -204;  
					yyd[39] = -197;  
					yyd[40] = 0;  
					yyd[41] = -211;  
					yyd[42] = 0;  
					yyd[43] = 0;  
					yyd[44] = 0;  
					yyd[45] = -194;  
					yyd[46] = 0;  
					yyd[47] = -209;  
					yyd[48] = 0;  
					yyd[49] = -190;  
					yyd[50] = 0;  
					yyd[51] = -191;  
					yyd[52] = -207;  
					yyd[53] = -185;  
					yyd[54] = 0;  
					yyd[55] = -217;  
					yyd[56] = 0;  
					yyd[57] = -198;  
					yyd[58] = -201;  
					yyd[59] = -196;  
					yyd[60] = -203;  
					yyd[61] = -200;  
					yyd[62] = -210;  
					yyd[63] = -193;  
					yyd[64] = -192;  
					yyd[65] = -195;  
					yyd[66] = -206;  
					yyd[67] = -208;  
					yyd[68] = -205;  
					yyd[69] = -216;  
					yyd[70] = 0;  
					yyd[71] = -219;  
					yyd[72] = 0;  
					yyd[73] = -218;  
					yyd[74] = 0;  
					yyd[75] = -96;  
					yyd[76] = 0;  
					yyd[77] = -2;  
					yyd[78] = -30;  
					yyd[79] = -29;  
					yyd[80] = 0;  
					yyd[81] = -189;  
					yyd[82] = -187;  
					yyd[83] = -188;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = 0;  
					yyd[87] = 0;  
					yyd[88] = -221;  
					yyd[89] = -152;  
					yyd[90] = -153;  
					yyd[91] = -170;  
					yyd[92] = -171;  
					yyd[93] = -169;  
					yyd[94] = -57;  
					yyd[95] = -61;  
					yyd[96] = -161;  
					yyd[97] = -168;  
					yyd[98] = -164;  
					yyd[99] = -163;  
					yyd[100] = -158;  
					yyd[101] = -166;  
					yyd[102] = -160;  
					yyd[103] = -167;  
					yyd[104] = -157;  
					yyd[105] = -165;  
					yyd[106] = -162;  
					yyd[107] = -159;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = -60;  
					yyd[111] = 0;  
					yyd[112] = -177;  
					yyd[113] = 0;  
					yyd[114] = -178;  
					yyd[115] = 0;  
					yyd[116] = -49;  
					yyd[117] = -50;  
					yyd[118] = -47;  
					yyd[119] = -48;  
					yyd[120] = -136;  
					yyd[121] = -137;  
					yyd[122] = -135;  
					yyd[123] = -182;  
					yyd[124] = -184;  
					yyd[125] = -222;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = -27;  
					yyd[131] = 0;  
					yyd[132] = -28;  
					yyd[133] = -36;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = -181;  
					yyd[139] = -183;  
					yyd[140] = -38;  
					yyd[141] = 0;  
					yyd[142] = -37;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = -35;  
					yyd[148] = -34;  
					yyd[149] = -33;  
					yyd[150] = -32;  
					yyd[151] = -31;  
					yyd[152] = 0;  
					yyd[153] = -155;  
					yyd[154] = -186;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = -88;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = -87;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = 0;  
					yyd[175] = 0;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = -59;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = 0;  
					yyd[187] = -46;  
					yyd[188] = -14;  
					yyd[189] = 0;  
					yyd[190] = -17;  
					yyd[191] = -15;  
					yyd[192] = -156;  
					yyd[193] = -26;  
					yyd[194] = -85;  
					yyd[195] = 0;  
					yyd[196] = -86;  
					yyd[197] = -81;  
					yyd[198] = -79;  
					yyd[199] = 0;  
					yyd[200] = 0;  
					yyd[201] = -74;  
					yyd[202] = -80;  
					yyd[203] = 0;  
					yyd[204] = -141;  
					yyd[205] = -142;  
					yyd[206] = -143;  
					yyd[207] = -144;  
					yyd[208] = -145;  
					yyd[209] = 0;  
					yyd[210] = 0;  
					yyd[211] = 0;  
					yyd[212] = -122;  
					yyd[213] = 0;  
					yyd[214] = 0;  
					yyd[215] = -117;  
					yyd[216] = -115;  
					yyd[217] = 0;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = 0;  
					yyd[222] = 0;  
					yyd[223] = 0;  
					yyd[224] = 0;  
					yyd[225] = 0;  
					yyd[226] = -138;  
					yyd[227] = -121;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = 0;  
					yyd[231] = 0;  
					yyd[232] = 0;  
					yyd[233] = -180;  
					yyd[234] = -179;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = -123;  
					yyd[238] = 0;  
					yyd[239] = 0;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = -58;  
					yyd[243] = -65;  
					yyd[244] = 0;  
					yyd[245] = 0;  
					yyd[246] = 0;  
					yyd[247] = -56;  
					yyd[248] = 0;  
					yyd[249] = 0;  
					yyd[250] = -55;  
					yyd[251] = 0;  
					yyd[252] = 0;  
					yyd[253] = 0;  
					yyd[254] = -91;  
					yyd[255] = -94;  
					yyd[256] = -95;  
					yyd[257] = -92;  
					yyd[258] = -93;  
					yyd[259] = -78;  
					yyd[260] = 0;  
					yyd[261] = -140;  
					yyd[262] = 0;  
					yyd[263] = -18;  
					yyd[264] = -19;  
					yyd[265] = 0;  
					yyd[266] = 0;  
					yyd[267] = -118;  
					yyd[268] = 0;  
					yyd[269] = -132;  
					yyd[270] = -133;  
					yyd[271] = -134;  
					yyd[272] = 0;  
					yyd[273] = -130;  
					yyd[274] = -131;  
					yyd[275] = 0;  
					yyd[276] = -126;  
					yyd[277] = -127;  
					yyd[278] = -128;  
					yyd[279] = -129;  
					yyd[280] = 0;  
					yyd[281] = -124;  
					yyd[282] = -125;  
					yyd[283] = 0;  
					yyd[284] = 0;  
					yyd[285] = 0;  
					yyd[286] = 0;  
					yyd[287] = 0;  
					yyd[288] = 0;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = 0;  
					yyd[292] = 0;  
					yyd[293] = -76;  
					yyd[294] = 0;  
					yyd[295] = -67;  
					yyd[296] = -71;  
					yyd[297] = -72;  
					yyd[298] = -73;  
					yyd[299] = -70;  
					yyd[300] = -64;  
					yyd[301] = 0;  
					yyd[302] = 0;  
					yyd[303] = 0;  
					yyd[304] = -51;  
					yyd[305] = 0;  
					yyd[306] = 0;  
					yyd[307] = -83;  
					yyd[308] = -84;  
					yyd[309] = 0;  
					yyd[310] = -21;  
					yyd[311] = -77;  
					yyd[312] = 0;  
					yyd[313] = -116;  
					yyd[314] = 0;  
					yyd[315] = 0;  
					yyd[316] = 0;  
					yyd[317] = 0;  
					yyd[318] = 0;  
					yyd[319] = 0;  
					yyd[320] = 0;  
					yyd[321] = 0;  
					yyd[322] = -139;  
					yyd[323] = -120;  
					yyd[324] = -149;  
					yyd[325] = 0;  
					yyd[326] = 0;  
					yyd[327] = 0;  
					yyd[328] = 0;  
					yyd[329] = 0;  
					yyd[330] = 0;  
					yyd[331] = -52;  
					yyd[332] = -16;  
					yyd[333] = -90;  
					yyd[334] = 0;  
					yyd[335] = -119;  
					yyd[336] = -150;  
					yyd[337] = -151;  
					yyd[338] = -69;  
					yyd[339] = 0;  
					yyd[340] = -25;  
					yyd[341] = -22;  
					yyd[342] = -23;  
					yyd[343] = -20;  
					yyd[344] = 0;  
					yyd[345] = 0;  
					yyd[346] = -24; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 33;  
					yyal[2] = 33;  
					yyal[3] = 33;  
					yyal[4] = 92;  
					yyal[5] = 116;  
					yyal[6] = 116;  
					yyal[7] = 140;  
					yyal[8] = 165;  
					yyal[9] = 174;  
					yyal[10] = 199;  
					yyal[11] = 199;  
					yyal[12] = 199;  
					yyal[13] = 199;  
					yyal[14] = 199;  
					yyal[15] = 199;  
					yyal[16] = 199;  
					yyal[17] = 199;  
					yyal[18] = 199;  
					yyal[19] = 199;  
					yyal[20] = 199;  
					yyal[21] = 233;  
					yyal[22] = 233;  
					yyal[23] = 234;  
					yyal[24] = 238;  
					yyal[25] = 238;  
					yyal[26] = 242;  
					yyal[27] = 246;  
					yyal[28] = 250;  
					yyal[29] = 251;  
					yyal[30] = 251;  
					yyal[31] = 251;  
					yyal[32] = 280;  
					yyal[33] = 293;  
					yyal[34] = 322;  
					yyal[35] = 350;  
					yyal[36] = 378;  
					yyal[37] = 378;  
					yyal[38] = 378;  
					yyal[39] = 378;  
					yyal[40] = 378;  
					yyal[41] = 391;  
					yyal[42] = 391;  
					yyal[43] = 404;  
					yyal[44] = 433;  
					yyal[45] = 461;  
					yyal[46] = 461;  
					yyal[47] = 474;  
					yyal[48] = 474;  
					yyal[49] = 487;  
					yyal[50] = 487;  
					yyal[51] = 500;  
					yyal[52] = 500;  
					yyal[53] = 500;  
					yyal[54] = 500;  
					yyal[55] = 515;  
					yyal[56] = 515;  
					yyal[57] = 517;  
					yyal[58] = 517;  
					yyal[59] = 517;  
					yyal[60] = 517;  
					yyal[61] = 517;  
					yyal[62] = 517;  
					yyal[63] = 517;  
					yyal[64] = 517;  
					yyal[65] = 517;  
					yyal[66] = 517;  
					yyal[67] = 517;  
					yyal[68] = 517;  
					yyal[69] = 517;  
					yyal[70] = 517;  
					yyal[71] = 521;  
					yyal[72] = 521;  
					yyal[73] = 523;  
					yyal[74] = 523;  
					yyal[75] = 531;  
					yyal[76] = 531;  
					yyal[77] = 555;  
					yyal[78] = 555;  
					yyal[79] = 555;  
					yyal[80] = 555;  
					yyal[81] = 556;  
					yyal[82] = 556;  
					yyal[83] = 556;  
					yyal[84] = 556;  
					yyal[85] = 557;  
					yyal[86] = 559;  
					yyal[87] = 560;  
					yyal[88] = 561;  
					yyal[89] = 561;  
					yyal[90] = 561;  
					yyal[91] = 561;  
					yyal[92] = 561;  
					yyal[93] = 561;  
					yyal[94] = 561;  
					yyal[95] = 561;  
					yyal[96] = 561;  
					yyal[97] = 561;  
					yyal[98] = 561;  
					yyal[99] = 561;  
					yyal[100] = 561;  
					yyal[101] = 561;  
					yyal[102] = 561;  
					yyal[103] = 561;  
					yyal[104] = 561;  
					yyal[105] = 561;  
					yyal[106] = 561;  
					yyal[107] = 561;  
					yyal[108] = 561;  
					yyal[109] = 562;  
					yyal[110] = 564;  
					yyal[111] = 564;  
					yyal[112] = 565;  
					yyal[113] = 565;  
					yyal[114] = 567;  
					yyal[115] = 567;  
					yyal[116] = 568;  
					yyal[117] = 568;  
					yyal[118] = 568;  
					yyal[119] = 568;  
					yyal[120] = 568;  
					yyal[121] = 568;  
					yyal[122] = 568;  
					yyal[123] = 568;  
					yyal[124] = 568;  
					yyal[125] = 568;  
					yyal[126] = 568;  
					yyal[127] = 594;  
					yyal[128] = 595;  
					yyal[129] = 628;  
					yyal[130] = 661;  
					yyal[131] = 661;  
					yyal[132] = 693;  
					yyal[133] = 693;  
					yyal[134] = 693;  
					yyal[135] = 728;  
					yyal[136] = 729;  
					yyal[137] = 742;  
					yyal[138] = 749;  
					yyal[139] = 749;  
					yyal[140] = 749;  
					yyal[141] = 749;  
					yyal[142] = 774;  
					yyal[143] = 774;  
					yyal[144] = 775;  
					yyal[145] = 777;  
					yyal[146] = 778;  
					yyal[147] = 804;  
					yyal[148] = 804;  
					yyal[149] = 804;  
					yyal[150] = 804;  
					yyal[151] = 804;  
					yyal[152] = 804;  
					yyal[153] = 805;  
					yyal[154] = 805;  
					yyal[155] = 805;  
					yyal[156] = 812;  
					yyal[157] = 813;  
					yyal[158] = 847;  
					yyal[159] = 884;  
					yyal[160] = 921;  
					yyal[161] = 922;  
					yyal[162] = 962;  
					yyal[163] = 963;  
					yyal[164] = 1000;  
					yyal[165] = 1000;  
					yyal[166] = 1005;  
					yyal[167] = 1009;  
					yyal[168] = 1009;  
					yyal[169] = 1013;  
					yyal[170] = 1048;  
					yyal[171] = 1079;  
					yyal[172] = 1114;  
					yyal[173] = 1145;  
					yyal[174] = 1176;  
					yyal[175] = 1217;  
					yyal[176] = 1258;  
					yyal[177] = 1299;  
					yyal[178] = 1340;  
					yyal[179] = 1381;  
					yyal[180] = 1381;  
					yyal[181] = 1413;  
					yyal[182] = 1414;  
					yyal[183] = 1415;  
					yyal[184] = 1430;  
					yyal[185] = 1434;  
					yyal[186] = 1438;  
					yyal[187] = 1444;  
					yyal[188] = 1444;  
					yyal[189] = 1444;  
					yyal[190] = 1445;  
					yyal[191] = 1445;  
					yyal[192] = 1445;  
					yyal[193] = 1445;  
					yyal[194] = 1445;  
					yyal[195] = 1445;  
					yyal[196] = 1477;  
					yyal[197] = 1477;  
					yyal[198] = 1477;  
					yyal[199] = 1477;  
					yyal[200] = 1514;  
					yyal[201] = 1546;  
					yyal[202] = 1546;  
					yyal[203] = 1546;  
					yyal[204] = 1577;  
					yyal[205] = 1577;  
					yyal[206] = 1577;  
					yyal[207] = 1577;  
					yyal[208] = 1577;  
					yyal[209] = 1577;  
					yyal[210] = 1613;  
					yyal[211] = 1649;  
					yyal[212] = 1650;  
					yyal[213] = 1650;  
					yyal[214] = 1651;  
					yyal[215] = 1682;  
					yyal[216] = 1682;  
					yyal[217] = 1682;  
					yyal[218] = 1701;  
					yyal[219] = 1717;  
					yyal[220] = 1731;  
					yyal[221] = 1741;  
					yyal[222] = 1749;  
					yyal[223] = 1756;  
					yyal[224] = 1762;  
					yyal[225] = 1767;  
					yyal[226] = 1771;  
					yyal[227] = 1771;  
					yyal[228] = 1771;  
					yyal[229] = 1793;  
					yyal[230] = 1824;  
					yyal[231] = 1850;  
					yyal[232] = 1876;  
					yyal[233] = 1902;  
					yyal[234] = 1902;  
					yyal[235] = 1902;  
					yyal[236] = 1937;  
					yyal[237] = 1938;  
					yyal[238] = 1938;  
					yyal[239] = 1939;  
					yyal[240] = 1976;  
					yyal[241] = 2007;  
					yyal[242] = 2022;  
					yyal[243] = 2022;  
					yyal[244] = 2022;  
					yyal[245] = 2023;  
					yyal[246] = 2024;  
					yyal[247] = 2025;  
					yyal[248] = 2025;  
					yyal[249] = 2027;  
					yyal[250] = 2028;  
					yyal[251] = 2028;  
					yyal[252] = 2060;  
					yyal[253] = 2094;  
					yyal[254] = 2095;  
					yyal[255] = 2095;  
					yyal[256] = 2095;  
					yyal[257] = 2095;  
					yyal[258] = 2095;  
					yyal[259] = 2095;  
					yyal[260] = 2095;  
					yyal[261] = 2096;  
					yyal[262] = 2096;  
					yyal[263] = 2098;  
					yyal[264] = 2098;  
					yyal[265] = 2098;  
					yyal[266] = 2135;  
					yyal[267] = 2166;  
					yyal[268] = 2166;  
					yyal[269] = 2197;  
					yyal[270] = 2197;  
					yyal[271] = 2197;  
					yyal[272] = 2197;  
					yyal[273] = 2228;  
					yyal[274] = 2228;  
					yyal[275] = 2228;  
					yyal[276] = 2259;  
					yyal[277] = 2259;  
					yyal[278] = 2259;  
					yyal[279] = 2259;  
					yyal[280] = 2259;  
					yyal[281] = 2290;  
					yyal[282] = 2290;  
					yyal[283] = 2290;  
					yyal[284] = 2321;  
					yyal[285] = 2352;  
					yyal[286] = 2383;  
					yyal[287] = 2414;  
					yyal[288] = 2445;  
					yyal[289] = 2476;  
					yyal[290] = 2477;  
					yyal[291] = 2478;  
					yyal[292] = 2513;  
					yyal[293] = 2548;  
					yyal[294] = 2548;  
					yyal[295] = 2581;  
					yyal[296] = 2581;  
					yyal[297] = 2581;  
					yyal[298] = 2581;  
					yyal[299] = 2581;  
					yyal[300] = 2581;  
					yyal[301] = 2581;  
					yyal[302] = 2595;  
					yyal[303] = 2609;  
					yyal[304] = 2615;  
					yyal[305] = 2615;  
					yyal[306] = 2616;  
					yyal[307] = 2649;  
					yyal[308] = 2649;  
					yyal[309] = 2649;  
					yyal[310] = 2684;  
					yyal[311] = 2684;  
					yyal[312] = 2684;  
					yyal[313] = 2685;  
					yyal[314] = 2685;  
					yyal[315] = 2704;  
					yyal[316] = 2720;  
					yyal[317] = 2734;  
					yyal[318] = 2744;  
					yyal[319] = 2752;  
					yyal[320] = 2759;  
					yyal[321] = 2765;  
					yyal[322] = 2770;  
					yyal[323] = 2770;  
					yyal[324] = 2770;  
					yyal[325] = 2770;  
					yyal[326] = 2771;  
					yyal[327] = 2772;  
					yyal[328] = 2804;  
					yyal[329] = 2806;  
					yyal[330] = 2807;  
					yyal[331] = 2808;  
					yyal[332] = 2808;  
					yyal[333] = 2808;  
					yyal[334] = 2808;  
					yyal[335] = 2809;  
					yyal[336] = 2809;  
					yyal[337] = 2809;  
					yyal[338] = 2809;  
					yyal[339] = 2809;  
					yyal[340] = 2810;  
					yyal[341] = 2810;  
					yyal[342] = 2810;  
					yyal[343] = 2810;  
					yyal[344] = 2810;  
					yyal[345] = 2823;  
					yyal[346] = 2824; 

					yyah = new int[yynstates];
					yyah[0] = 32;  
					yyah[1] = 32;  
					yyah[2] = 32;  
					yyah[3] = 91;  
					yyah[4] = 115;  
					yyah[5] = 115;  
					yyah[6] = 139;  
					yyah[7] = 164;  
					yyah[8] = 173;  
					yyah[9] = 198;  
					yyah[10] = 198;  
					yyah[11] = 198;  
					yyah[12] = 198;  
					yyah[13] = 198;  
					yyah[14] = 198;  
					yyah[15] = 198;  
					yyah[16] = 198;  
					yyah[17] = 198;  
					yyah[18] = 198;  
					yyah[19] = 198;  
					yyah[20] = 232;  
					yyah[21] = 232;  
					yyah[22] = 233;  
					yyah[23] = 237;  
					yyah[24] = 237;  
					yyah[25] = 241;  
					yyah[26] = 245;  
					yyah[27] = 249;  
					yyah[28] = 250;  
					yyah[29] = 250;  
					yyah[30] = 250;  
					yyah[31] = 279;  
					yyah[32] = 292;  
					yyah[33] = 321;  
					yyah[34] = 349;  
					yyah[35] = 377;  
					yyah[36] = 377;  
					yyah[37] = 377;  
					yyah[38] = 377;  
					yyah[39] = 377;  
					yyah[40] = 390;  
					yyah[41] = 390;  
					yyah[42] = 403;  
					yyah[43] = 432;  
					yyah[44] = 460;  
					yyah[45] = 460;  
					yyah[46] = 473;  
					yyah[47] = 473;  
					yyah[48] = 486;  
					yyah[49] = 486;  
					yyah[50] = 499;  
					yyah[51] = 499;  
					yyah[52] = 499;  
					yyah[53] = 499;  
					yyah[54] = 514;  
					yyah[55] = 514;  
					yyah[56] = 516;  
					yyah[57] = 516;  
					yyah[58] = 516;  
					yyah[59] = 516;  
					yyah[60] = 516;  
					yyah[61] = 516;  
					yyah[62] = 516;  
					yyah[63] = 516;  
					yyah[64] = 516;  
					yyah[65] = 516;  
					yyah[66] = 516;  
					yyah[67] = 516;  
					yyah[68] = 516;  
					yyah[69] = 516;  
					yyah[70] = 520;  
					yyah[71] = 520;  
					yyah[72] = 522;  
					yyah[73] = 522;  
					yyah[74] = 530;  
					yyah[75] = 530;  
					yyah[76] = 554;  
					yyah[77] = 554;  
					yyah[78] = 554;  
					yyah[79] = 554;  
					yyah[80] = 555;  
					yyah[81] = 555;  
					yyah[82] = 555;  
					yyah[83] = 555;  
					yyah[84] = 556;  
					yyah[85] = 558;  
					yyah[86] = 559;  
					yyah[87] = 560;  
					yyah[88] = 560;  
					yyah[89] = 560;  
					yyah[90] = 560;  
					yyah[91] = 560;  
					yyah[92] = 560;  
					yyah[93] = 560;  
					yyah[94] = 560;  
					yyah[95] = 560;  
					yyah[96] = 560;  
					yyah[97] = 560;  
					yyah[98] = 560;  
					yyah[99] = 560;  
					yyah[100] = 560;  
					yyah[101] = 560;  
					yyah[102] = 560;  
					yyah[103] = 560;  
					yyah[104] = 560;  
					yyah[105] = 560;  
					yyah[106] = 560;  
					yyah[107] = 560;  
					yyah[108] = 561;  
					yyah[109] = 563;  
					yyah[110] = 563;  
					yyah[111] = 564;  
					yyah[112] = 564;  
					yyah[113] = 566;  
					yyah[114] = 566;  
					yyah[115] = 567;  
					yyah[116] = 567;  
					yyah[117] = 567;  
					yyah[118] = 567;  
					yyah[119] = 567;  
					yyah[120] = 567;  
					yyah[121] = 567;  
					yyah[122] = 567;  
					yyah[123] = 567;  
					yyah[124] = 567;  
					yyah[125] = 567;  
					yyah[126] = 593;  
					yyah[127] = 594;  
					yyah[128] = 627;  
					yyah[129] = 660;  
					yyah[130] = 660;  
					yyah[131] = 692;  
					yyah[132] = 692;  
					yyah[133] = 692;  
					yyah[134] = 727;  
					yyah[135] = 728;  
					yyah[136] = 741;  
					yyah[137] = 748;  
					yyah[138] = 748;  
					yyah[139] = 748;  
					yyah[140] = 748;  
					yyah[141] = 773;  
					yyah[142] = 773;  
					yyah[143] = 774;  
					yyah[144] = 776;  
					yyah[145] = 777;  
					yyah[146] = 803;  
					yyah[147] = 803;  
					yyah[148] = 803;  
					yyah[149] = 803;  
					yyah[150] = 803;  
					yyah[151] = 803;  
					yyah[152] = 804;  
					yyah[153] = 804;  
					yyah[154] = 804;  
					yyah[155] = 811;  
					yyah[156] = 812;  
					yyah[157] = 846;  
					yyah[158] = 883;  
					yyah[159] = 920;  
					yyah[160] = 921;  
					yyah[161] = 961;  
					yyah[162] = 962;  
					yyah[163] = 999;  
					yyah[164] = 999;  
					yyah[165] = 1004;  
					yyah[166] = 1008;  
					yyah[167] = 1008;  
					yyah[168] = 1012;  
					yyah[169] = 1047;  
					yyah[170] = 1078;  
					yyah[171] = 1113;  
					yyah[172] = 1144;  
					yyah[173] = 1175;  
					yyah[174] = 1216;  
					yyah[175] = 1257;  
					yyah[176] = 1298;  
					yyah[177] = 1339;  
					yyah[178] = 1380;  
					yyah[179] = 1380;  
					yyah[180] = 1412;  
					yyah[181] = 1413;  
					yyah[182] = 1414;  
					yyah[183] = 1429;  
					yyah[184] = 1433;  
					yyah[185] = 1437;  
					yyah[186] = 1443;  
					yyah[187] = 1443;  
					yyah[188] = 1443;  
					yyah[189] = 1444;  
					yyah[190] = 1444;  
					yyah[191] = 1444;  
					yyah[192] = 1444;  
					yyah[193] = 1444;  
					yyah[194] = 1444;  
					yyah[195] = 1476;  
					yyah[196] = 1476;  
					yyah[197] = 1476;  
					yyah[198] = 1476;  
					yyah[199] = 1513;  
					yyah[200] = 1545;  
					yyah[201] = 1545;  
					yyah[202] = 1545;  
					yyah[203] = 1576;  
					yyah[204] = 1576;  
					yyah[205] = 1576;  
					yyah[206] = 1576;  
					yyah[207] = 1576;  
					yyah[208] = 1576;  
					yyah[209] = 1612;  
					yyah[210] = 1648;  
					yyah[211] = 1649;  
					yyah[212] = 1649;  
					yyah[213] = 1650;  
					yyah[214] = 1681;  
					yyah[215] = 1681;  
					yyah[216] = 1681;  
					yyah[217] = 1700;  
					yyah[218] = 1716;  
					yyah[219] = 1730;  
					yyah[220] = 1740;  
					yyah[221] = 1748;  
					yyah[222] = 1755;  
					yyah[223] = 1761;  
					yyah[224] = 1766;  
					yyah[225] = 1770;  
					yyah[226] = 1770;  
					yyah[227] = 1770;  
					yyah[228] = 1792;  
					yyah[229] = 1823;  
					yyah[230] = 1849;  
					yyah[231] = 1875;  
					yyah[232] = 1901;  
					yyah[233] = 1901;  
					yyah[234] = 1901;  
					yyah[235] = 1936;  
					yyah[236] = 1937;  
					yyah[237] = 1937;  
					yyah[238] = 1938;  
					yyah[239] = 1975;  
					yyah[240] = 2006;  
					yyah[241] = 2021;  
					yyah[242] = 2021;  
					yyah[243] = 2021;  
					yyah[244] = 2022;  
					yyah[245] = 2023;  
					yyah[246] = 2024;  
					yyah[247] = 2024;  
					yyah[248] = 2026;  
					yyah[249] = 2027;  
					yyah[250] = 2027;  
					yyah[251] = 2059;  
					yyah[252] = 2093;  
					yyah[253] = 2094;  
					yyah[254] = 2094;  
					yyah[255] = 2094;  
					yyah[256] = 2094;  
					yyah[257] = 2094;  
					yyah[258] = 2094;  
					yyah[259] = 2094;  
					yyah[260] = 2095;  
					yyah[261] = 2095;  
					yyah[262] = 2097;  
					yyah[263] = 2097;  
					yyah[264] = 2097;  
					yyah[265] = 2134;  
					yyah[266] = 2165;  
					yyah[267] = 2165;  
					yyah[268] = 2196;  
					yyah[269] = 2196;  
					yyah[270] = 2196;  
					yyah[271] = 2196;  
					yyah[272] = 2227;  
					yyah[273] = 2227;  
					yyah[274] = 2227;  
					yyah[275] = 2258;  
					yyah[276] = 2258;  
					yyah[277] = 2258;  
					yyah[278] = 2258;  
					yyah[279] = 2258;  
					yyah[280] = 2289;  
					yyah[281] = 2289;  
					yyah[282] = 2289;  
					yyah[283] = 2320;  
					yyah[284] = 2351;  
					yyah[285] = 2382;  
					yyah[286] = 2413;  
					yyah[287] = 2444;  
					yyah[288] = 2475;  
					yyah[289] = 2476;  
					yyah[290] = 2477;  
					yyah[291] = 2512;  
					yyah[292] = 2547;  
					yyah[293] = 2547;  
					yyah[294] = 2580;  
					yyah[295] = 2580;  
					yyah[296] = 2580;  
					yyah[297] = 2580;  
					yyah[298] = 2580;  
					yyah[299] = 2580;  
					yyah[300] = 2580;  
					yyah[301] = 2594;  
					yyah[302] = 2608;  
					yyah[303] = 2614;  
					yyah[304] = 2614;  
					yyah[305] = 2615;  
					yyah[306] = 2648;  
					yyah[307] = 2648;  
					yyah[308] = 2648;  
					yyah[309] = 2683;  
					yyah[310] = 2683;  
					yyah[311] = 2683;  
					yyah[312] = 2684;  
					yyah[313] = 2684;  
					yyah[314] = 2703;  
					yyah[315] = 2719;  
					yyah[316] = 2733;  
					yyah[317] = 2743;  
					yyah[318] = 2751;  
					yyah[319] = 2758;  
					yyah[320] = 2764;  
					yyah[321] = 2769;  
					yyah[322] = 2769;  
					yyah[323] = 2769;  
					yyah[324] = 2769;  
					yyah[325] = 2770;  
					yyah[326] = 2771;  
					yyah[327] = 2803;  
					yyah[328] = 2805;  
					yyah[329] = 2806;  
					yyah[330] = 2807;  
					yyah[331] = 2807;  
					yyah[332] = 2807;  
					yyah[333] = 2807;  
					yyah[334] = 2808;  
					yyah[335] = 2808;  
					yyah[336] = 2808;  
					yyah[337] = 2808;  
					yyah[338] = 2808;  
					yyah[339] = 2809;  
					yyah[340] = 2809;  
					yyah[341] = 2809;  
					yyah[342] = 2809;  
					yyah[343] = 2809;  
					yyah[344] = 2822;  
					yyah[345] = 2823;  
					yyah[346] = 2823; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 23;  
					yygl[2] = 23;  
					yygl[3] = 23;  
					yygl[4] = 23;  
					yygl[5] = 28;  
					yygl[6] = 28;  
					yygl[7] = 33;  
					yygl[8] = 38;  
					yygl[9] = 39;  
					yygl[10] = 40;  
					yygl[11] = 40;  
					yygl[12] = 40;  
					yygl[13] = 40;  
					yygl[14] = 40;  
					yygl[15] = 40;  
					yygl[16] = 40;  
					yygl[17] = 40;  
					yygl[18] = 40;  
					yygl[19] = 40;  
					yygl[20] = 40;  
					yygl[21] = 61;  
					yygl[22] = 61;  
					yygl[23] = 61;  
					yygl[24] = 64;  
					yygl[25] = 64;  
					yygl[26] = 67;  
					yygl[27] = 70;  
					yygl[28] = 73;  
					yygl[29] = 74;  
					yygl[30] = 74;  
					yygl[31] = 74;  
					yygl[32] = 74;  
					yygl[33] = 74;  
					yygl[34] = 74;  
					yygl[35] = 74;  
					yygl[36] = 74;  
					yygl[37] = 74;  
					yygl[38] = 74;  
					yygl[39] = 74;  
					yygl[40] = 74;  
					yygl[41] = 74;  
					yygl[42] = 74;  
					yygl[43] = 74;  
					yygl[44] = 74;  
					yygl[45] = 74;  
					yygl[46] = 74;  
					yygl[47] = 74;  
					yygl[48] = 74;  
					yygl[49] = 74;  
					yygl[50] = 74;  
					yygl[51] = 74;  
					yygl[52] = 74;  
					yygl[53] = 74;  
					yygl[54] = 74;  
					yygl[55] = 79;  
					yygl[56] = 79;  
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
					yygl[73] = 82;  
					yygl[74] = 82;  
					yygl[75] = 90;  
					yygl[76] = 90;  
					yygl[77] = 96;  
					yygl[78] = 96;  
					yygl[79] = 96;  
					yygl[80] = 96;  
					yygl[81] = 96;  
					yygl[82] = 96;  
					yygl[83] = 96;  
					yygl[84] = 96;  
					yygl[85] = 96;  
					yygl[86] = 96;  
					yygl[87] = 96;  
					yygl[88] = 96;  
					yygl[89] = 96;  
					yygl[90] = 96;  
					yygl[91] = 96;  
					yygl[92] = 96;  
					yygl[93] = 96;  
					yygl[94] = 96;  
					yygl[95] = 96;  
					yygl[96] = 96;  
					yygl[97] = 96;  
					yygl[98] = 96;  
					yygl[99] = 96;  
					yygl[100] = 96;  
					yygl[101] = 96;  
					yygl[102] = 96;  
					yygl[103] = 96;  
					yygl[104] = 96;  
					yygl[105] = 96;  
					yygl[106] = 96;  
					yygl[107] = 96;  
					yygl[108] = 96;  
					yygl[109] = 96;  
					yygl[110] = 97;  
					yygl[111] = 97;  
					yygl[112] = 98;  
					yygl[113] = 98;  
					yygl[114] = 98;  
					yygl[115] = 98;  
					yygl[116] = 98;  
					yygl[117] = 98;  
					yygl[118] = 98;  
					yygl[119] = 98;  
					yygl[120] = 98;  
					yygl[121] = 98;  
					yygl[122] = 98;  
					yygl[123] = 98;  
					yygl[124] = 98;  
					yygl[125] = 98;  
					yygl[126] = 98;  
					yygl[127] = 99;  
					yygl[128] = 99;  
					yygl[129] = 121;  
					yygl[130] = 143;  
					yygl[131] = 143;  
					yygl[132] = 157;  
					yygl[133] = 157;  
					yygl[134] = 157;  
					yygl[135] = 170;  
					yygl[136] = 170;  
					yygl[137] = 177;  
					yygl[138] = 178;  
					yygl[139] = 178;  
					yygl[140] = 178;  
					yygl[141] = 178;  
					yygl[142] = 184;  
					yygl[143] = 184;  
					yygl[144] = 184;  
					yygl[145] = 184;  
					yygl[146] = 184;  
					yygl[147] = 189;  
					yygl[148] = 189;  
					yygl[149] = 189;  
					yygl[150] = 189;  
					yygl[151] = 189;  
					yygl[152] = 189;  
					yygl[153] = 189;  
					yygl[154] = 189;  
					yygl[155] = 189;  
					yygl[156] = 189;  
					yygl[157] = 189;  
					yygl[158] = 190;  
					yygl[159] = 203;  
					yygl[160] = 216;  
					yygl[161] = 216;  
					yygl[162] = 217;  
					yygl[163] = 217;  
					yygl[164] = 230;  
					yygl[165] = 230;  
					yygl[166] = 231;  
					yygl[167] = 234;  
					yygl[168] = 234;  
					yygl[169] = 237;  
					yygl[170] = 250;  
					yygl[171] = 271;  
					yygl[172] = 271;  
					yygl[173] = 292;  
					yygl[174] = 313;  
					yygl[175] = 313;  
					yygl[176] = 313;  
					yygl[177] = 313;  
					yygl[178] = 313;  
					yygl[179] = 313;  
					yygl[180] = 313;  
					yygl[181] = 314;  
					yygl[182] = 314;  
					yygl[183] = 314;  
					yygl[184] = 321;  
					yygl[185] = 324;  
					yygl[186] = 327;  
					yygl[187] = 332;  
					yygl[188] = 332;  
					yygl[189] = 332;  
					yygl[190] = 332;  
					yygl[191] = 332;  
					yygl[192] = 332;  
					yygl[193] = 332;  
					yygl[194] = 332;  
					yygl[195] = 332;  
					yygl[196] = 347;  
					yygl[197] = 347;  
					yygl[198] = 347;  
					yygl[199] = 347;  
					yygl[200] = 360;  
					yygl[201] = 375;  
					yygl[202] = 375;  
					yygl[203] = 375;  
					yygl[204] = 396;  
					yygl[205] = 396;  
					yygl[206] = 396;  
					yygl[207] = 396;  
					yygl[208] = 396;  
					yygl[209] = 396;  
					yygl[210] = 410;  
					yygl[211] = 424;  
					yygl[212] = 424;  
					yygl[213] = 424;  
					yygl[214] = 424;  
					yygl[215] = 435;  
					yygl[216] = 435;  
					yygl[217] = 435;  
					yygl[218] = 436;  
					yygl[219] = 437;  
					yygl[220] = 438;  
					yygl[221] = 439;  
					yygl[222] = 439;  
					yygl[223] = 439;  
					yygl[224] = 439;  
					yygl[225] = 439;  
					yygl[226] = 439;  
					yygl[227] = 439;  
					yygl[228] = 439;  
					yygl[229] = 440;  
					yygl[230] = 461;  
					yygl[231] = 461;  
					yygl[232] = 461;  
					yygl[233] = 461;  
					yygl[234] = 461;  
					yygl[235] = 461;  
					yygl[236] = 474;  
					yygl[237] = 474;  
					yygl[238] = 474;  
					yygl[239] = 474;  
					yygl[240] = 487;  
					yygl[241] = 501;  
					yygl[242] = 508;  
					yygl[243] = 508;  
					yygl[244] = 508;  
					yygl[245] = 508;  
					yygl[246] = 508;  
					yygl[247] = 508;  
					yygl[248] = 508;  
					yygl[249] = 508;  
					yygl[250] = 508;  
					yygl[251] = 508;  
					yygl[252] = 529;  
					yygl[253] = 530;  
					yygl[254] = 530;  
					yygl[255] = 530;  
					yygl[256] = 530;  
					yygl[257] = 530;  
					yygl[258] = 530;  
					yygl[259] = 530;  
					yygl[260] = 530;  
					yygl[261] = 530;  
					yygl[262] = 530;  
					yygl[263] = 530;  
					yygl[264] = 530;  
					yygl[265] = 530;  
					yygl[266] = 543;  
					yygl[267] = 564;  
					yygl[268] = 564;  
					yygl[269] = 575;  
					yygl[270] = 575;  
					yygl[271] = 575;  
					yygl[272] = 575;  
					yygl[273] = 587;  
					yygl[274] = 587;  
					yygl[275] = 587;  
					yygl[276] = 600;  
					yygl[277] = 600;  
					yygl[278] = 600;  
					yygl[279] = 600;  
					yygl[280] = 600;  
					yygl[281] = 614;  
					yygl[282] = 614;  
					yygl[283] = 614;  
					yygl[284] = 629;  
					yygl[285] = 645;  
					yygl[286] = 662;  
					yygl[287] = 680;  
					yygl[288] = 699;  
					yygl[289] = 720;  
					yygl[290] = 720;  
					yygl[291] = 720;  
					yygl[292] = 733;  
					yygl[293] = 746;  
					yygl[294] = 746;  
					yygl[295] = 747;  
					yygl[296] = 747;  
					yygl[297] = 747;  
					yygl[298] = 747;  
					yygl[299] = 747;  
					yygl[300] = 747;  
					yygl[301] = 747;  
					yygl[302] = 755;  
					yygl[303] = 763;  
					yygl[304] = 768;  
					yygl[305] = 768;  
					yygl[306] = 768;  
					yygl[307] = 783;  
					yygl[308] = 783;  
					yygl[309] = 783;  
					yygl[310] = 796;  
					yygl[311] = 796;  
					yygl[312] = 796;  
					yygl[313] = 796;  
					yygl[314] = 796;  
					yygl[315] = 797;  
					yygl[316] = 798;  
					yygl[317] = 799;  
					yygl[318] = 800;  
					yygl[319] = 800;  
					yygl[320] = 800;  
					yygl[321] = 800;  
					yygl[322] = 800;  
					yygl[323] = 800;  
					yygl[324] = 800;  
					yygl[325] = 800;  
					yygl[326] = 800;  
					yygl[327] = 800;  
					yygl[328] = 814;  
					yygl[329] = 814;  
					yygl[330] = 814;  
					yygl[331] = 814;  
					yygl[332] = 814;  
					yygl[333] = 814;  
					yygl[334] = 814;  
					yygl[335] = 814;  
					yygl[336] = 814;  
					yygl[337] = 814;  
					yygl[338] = 814;  
					yygl[339] = 814;  
					yygl[340] = 814;  
					yygl[341] = 814;  
					yygl[342] = 814;  
					yygl[343] = 814;  
					yygl[344] = 814;  
					yygl[345] = 821;  
					yygl[346] = 821; 

					yygh = new int[yynstates];
					yygh[0] = 22;  
					yygh[1] = 22;  
					yygh[2] = 22;  
					yygh[3] = 22;  
					yygh[4] = 27;  
					yygh[5] = 27;  
					yygh[6] = 32;  
					yygh[7] = 37;  
					yygh[8] = 38;  
					yygh[9] = 39;  
					yygh[10] = 39;  
					yygh[11] = 39;  
					yygh[12] = 39;  
					yygh[13] = 39;  
					yygh[14] = 39;  
					yygh[15] = 39;  
					yygh[16] = 39;  
					yygh[17] = 39;  
					yygh[18] = 39;  
					yygh[19] = 39;  
					yygh[20] = 60;  
					yygh[21] = 60;  
					yygh[22] = 60;  
					yygh[23] = 63;  
					yygh[24] = 63;  
					yygh[25] = 66;  
					yygh[26] = 69;  
					yygh[27] = 72;  
					yygh[28] = 73;  
					yygh[29] = 73;  
					yygh[30] = 73;  
					yygh[31] = 73;  
					yygh[32] = 73;  
					yygh[33] = 73;  
					yygh[34] = 73;  
					yygh[35] = 73;  
					yygh[36] = 73;  
					yygh[37] = 73;  
					yygh[38] = 73;  
					yygh[39] = 73;  
					yygh[40] = 73;  
					yygh[41] = 73;  
					yygh[42] = 73;  
					yygh[43] = 73;  
					yygh[44] = 73;  
					yygh[45] = 73;  
					yygh[46] = 73;  
					yygh[47] = 73;  
					yygh[48] = 73;  
					yygh[49] = 73;  
					yygh[50] = 73;  
					yygh[51] = 73;  
					yygh[52] = 73;  
					yygh[53] = 73;  
					yygh[54] = 78;  
					yygh[55] = 78;  
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
					yygh[72] = 81;  
					yygh[73] = 81;  
					yygh[74] = 89;  
					yygh[75] = 89;  
					yygh[76] = 95;  
					yygh[77] = 95;  
					yygh[78] = 95;  
					yygh[79] = 95;  
					yygh[80] = 95;  
					yygh[81] = 95;  
					yygh[82] = 95;  
					yygh[83] = 95;  
					yygh[84] = 95;  
					yygh[85] = 95;  
					yygh[86] = 95;  
					yygh[87] = 95;  
					yygh[88] = 95;  
					yygh[89] = 95;  
					yygh[90] = 95;  
					yygh[91] = 95;  
					yygh[92] = 95;  
					yygh[93] = 95;  
					yygh[94] = 95;  
					yygh[95] = 95;  
					yygh[96] = 95;  
					yygh[97] = 95;  
					yygh[98] = 95;  
					yygh[99] = 95;  
					yygh[100] = 95;  
					yygh[101] = 95;  
					yygh[102] = 95;  
					yygh[103] = 95;  
					yygh[104] = 95;  
					yygh[105] = 95;  
					yygh[106] = 95;  
					yygh[107] = 95;  
					yygh[108] = 95;  
					yygh[109] = 96;  
					yygh[110] = 96;  
					yygh[111] = 97;  
					yygh[112] = 97;  
					yygh[113] = 97;  
					yygh[114] = 97;  
					yygh[115] = 97;  
					yygh[116] = 97;  
					yygh[117] = 97;  
					yygh[118] = 97;  
					yygh[119] = 97;  
					yygh[120] = 97;  
					yygh[121] = 97;  
					yygh[122] = 97;  
					yygh[123] = 97;  
					yygh[124] = 97;  
					yygh[125] = 97;  
					yygh[126] = 98;  
					yygh[127] = 98;  
					yygh[128] = 120;  
					yygh[129] = 142;  
					yygh[130] = 142;  
					yygh[131] = 156;  
					yygh[132] = 156;  
					yygh[133] = 156;  
					yygh[134] = 169;  
					yygh[135] = 169;  
					yygh[136] = 176;  
					yygh[137] = 177;  
					yygh[138] = 177;  
					yygh[139] = 177;  
					yygh[140] = 177;  
					yygh[141] = 183;  
					yygh[142] = 183;  
					yygh[143] = 183;  
					yygh[144] = 183;  
					yygh[145] = 183;  
					yygh[146] = 188;  
					yygh[147] = 188;  
					yygh[148] = 188;  
					yygh[149] = 188;  
					yygh[150] = 188;  
					yygh[151] = 188;  
					yygh[152] = 188;  
					yygh[153] = 188;  
					yygh[154] = 188;  
					yygh[155] = 188;  
					yygh[156] = 188;  
					yygh[157] = 189;  
					yygh[158] = 202;  
					yygh[159] = 215;  
					yygh[160] = 215;  
					yygh[161] = 216;  
					yygh[162] = 216;  
					yygh[163] = 229;  
					yygh[164] = 229;  
					yygh[165] = 230;  
					yygh[166] = 233;  
					yygh[167] = 233;  
					yygh[168] = 236;  
					yygh[169] = 249;  
					yygh[170] = 270;  
					yygh[171] = 270;  
					yygh[172] = 291;  
					yygh[173] = 312;  
					yygh[174] = 312;  
					yygh[175] = 312;  
					yygh[176] = 312;  
					yygh[177] = 312;  
					yygh[178] = 312;  
					yygh[179] = 312;  
					yygh[180] = 313;  
					yygh[181] = 313;  
					yygh[182] = 313;  
					yygh[183] = 320;  
					yygh[184] = 323;  
					yygh[185] = 326;  
					yygh[186] = 331;  
					yygh[187] = 331;  
					yygh[188] = 331;  
					yygh[189] = 331;  
					yygh[190] = 331;  
					yygh[191] = 331;  
					yygh[192] = 331;  
					yygh[193] = 331;  
					yygh[194] = 331;  
					yygh[195] = 346;  
					yygh[196] = 346;  
					yygh[197] = 346;  
					yygh[198] = 346;  
					yygh[199] = 359;  
					yygh[200] = 374;  
					yygh[201] = 374;  
					yygh[202] = 374;  
					yygh[203] = 395;  
					yygh[204] = 395;  
					yygh[205] = 395;  
					yygh[206] = 395;  
					yygh[207] = 395;  
					yygh[208] = 395;  
					yygh[209] = 409;  
					yygh[210] = 423;  
					yygh[211] = 423;  
					yygh[212] = 423;  
					yygh[213] = 423;  
					yygh[214] = 434;  
					yygh[215] = 434;  
					yygh[216] = 434;  
					yygh[217] = 435;  
					yygh[218] = 436;  
					yygh[219] = 437;  
					yygh[220] = 438;  
					yygh[221] = 438;  
					yygh[222] = 438;  
					yygh[223] = 438;  
					yygh[224] = 438;  
					yygh[225] = 438;  
					yygh[226] = 438;  
					yygh[227] = 438;  
					yygh[228] = 439;  
					yygh[229] = 460;  
					yygh[230] = 460;  
					yygh[231] = 460;  
					yygh[232] = 460;  
					yygh[233] = 460;  
					yygh[234] = 460;  
					yygh[235] = 473;  
					yygh[236] = 473;  
					yygh[237] = 473;  
					yygh[238] = 473;  
					yygh[239] = 486;  
					yygh[240] = 500;  
					yygh[241] = 507;  
					yygh[242] = 507;  
					yygh[243] = 507;  
					yygh[244] = 507;  
					yygh[245] = 507;  
					yygh[246] = 507;  
					yygh[247] = 507;  
					yygh[248] = 507;  
					yygh[249] = 507;  
					yygh[250] = 507;  
					yygh[251] = 528;  
					yygh[252] = 529;  
					yygh[253] = 529;  
					yygh[254] = 529;  
					yygh[255] = 529;  
					yygh[256] = 529;  
					yygh[257] = 529;  
					yygh[258] = 529;  
					yygh[259] = 529;  
					yygh[260] = 529;  
					yygh[261] = 529;  
					yygh[262] = 529;  
					yygh[263] = 529;  
					yygh[264] = 529;  
					yygh[265] = 542;  
					yygh[266] = 563;  
					yygh[267] = 563;  
					yygh[268] = 574;  
					yygh[269] = 574;  
					yygh[270] = 574;  
					yygh[271] = 574;  
					yygh[272] = 586;  
					yygh[273] = 586;  
					yygh[274] = 586;  
					yygh[275] = 599;  
					yygh[276] = 599;  
					yygh[277] = 599;  
					yygh[278] = 599;  
					yygh[279] = 599;  
					yygh[280] = 613;  
					yygh[281] = 613;  
					yygh[282] = 613;  
					yygh[283] = 628;  
					yygh[284] = 644;  
					yygh[285] = 661;  
					yygh[286] = 679;  
					yygh[287] = 698;  
					yygh[288] = 719;  
					yygh[289] = 719;  
					yygh[290] = 719;  
					yygh[291] = 732;  
					yygh[292] = 745;  
					yygh[293] = 745;  
					yygh[294] = 746;  
					yygh[295] = 746;  
					yygh[296] = 746;  
					yygh[297] = 746;  
					yygh[298] = 746;  
					yygh[299] = 746;  
					yygh[300] = 746;  
					yygh[301] = 754;  
					yygh[302] = 762;  
					yygh[303] = 767;  
					yygh[304] = 767;  
					yygh[305] = 767;  
					yygh[306] = 782;  
					yygh[307] = 782;  
					yygh[308] = 782;  
					yygh[309] = 795;  
					yygh[310] = 795;  
					yygh[311] = 795;  
					yygh[312] = 795;  
					yygh[313] = 795;  
					yygh[314] = 796;  
					yygh[315] = 797;  
					yygh[316] = 798;  
					yygh[317] = 799;  
					yygh[318] = 799;  
					yygh[319] = 799;  
					yygh[320] = 799;  
					yygh[321] = 799;  
					yygh[322] = 799;  
					yygh[323] = 799;  
					yygh[324] = 799;  
					yygh[325] = 799;  
					yygh[326] = 799;  
					yygh[327] = 813;  
					yygh[328] = 813;  
					yygh[329] = 813;  
					yygh[330] = 813;  
					yygh[331] = 813;  
					yygh[332] = 813;  
					yygh[333] = 813;  
					yygh[334] = 813;  
					yygh[335] = 813;  
					yygh[336] = 813;  
					yygh[337] = 813;  
					yygh[338] = 813;  
					yygh[339] = 813;  
					yygh[340] = 813;  
					yygh[341] = 813;  
					yygh[342] = 813;  
					yygh[343] = 813;  
					yygh[344] = 820;  
					yygh[345] = 820;  
					yygh[346] = 820; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
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
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-30);yyrc++; 
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
					yyr[yyrc] = new YYRRec(2,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++;
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
            //StreamReader in_s = File.OpenText(file);
            //string inputstream = in_s.ReadToEnd();
            //in_s.Close();
            string inputstream = File.ReadAllText(file, Encoding.ASCII);

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

			if (Regex.IsMatch(Rest,"^((:=|(,+[\\s\\t\\x00]*)*))")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^((:=|(,+[\\s\\t\\x00]*)*))").Value);}

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

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)").Value);}

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
