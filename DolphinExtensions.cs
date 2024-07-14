using AutoMapper;
using Dolphin.DAL;
using Dolphin.DAL.Enums;
using Dolphin.Injection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;

namespace Dolphin
{
    public static class DolphinExtensions
    {
        public static IServiceCollection AddDolphinServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.DefinedTypes
                                    .Where(x => x.IsClass && x.GetInterface($"I{x.Name}") != default).ToList();
            foreach (var type in types)
                services.RegisterService(type, assembly);

            var servers = assembly.DefinedTypes
                                   .Where(x => x.IsClass && x.GetInterface("IServer") != default).ToList();
            foreach (var server in servers)
                services.RegisterService(server, assembly);

            var packets = assembly.DefinedTypes
                                    .Where(x => x.IsClass && x.GetInterface("IIncomingHandler") != default).ToList();
            foreach (var packet in packets)
                services.RegisterService(packet, assembly);

            var consoleCommands = assembly.DefinedTypes
                                    .Where(x => x.IsClass && x.GetInterface("ICommand") != default).ToList();
            foreach (var consoleCommand in consoleCommands)
                services.RegisterService(consoleCommand, assembly);

            var tasks = assembly.DefinedTypes.Where(x => x.IsClass && x.GetInterface("ITask") != default).ToList();
            foreach (var task in tasks)
                services.RegisterService(task, assembly);

            services.AddDbContext<DolphinDbContext>(ServiceLifetime.Transient);

            services.AddAutoMapper(assembly);

            services.AddDbContextFactory<DolphinDbContext>(lifetime: ServiceLifetime.Transient);

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug).AddConsole();
            });

            return services;
        }

        public static void ConfigureMySQL(this DbContextOptionsBuilder builder, string connectionString, Action<MySqlDbContextOptionsBuilder>? options = null)
            => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options);

        public static IMappingExpression<E, R> MapProperties<E, R>(this IMappingExpression<E, R> mappingExpression, params (Expression<Func<R, object>> destinationMember, Expression<Func<E, object>> sourceMember)[] properties)
        {
            foreach (var (dest, src) in properties)
            {
                mappingExpression.ForMember(dest, opt => opt.MapFrom(src));
            }
            return mappingExpression;
        }

        internal static R GetMap<E, R>(this E @object, params (Expression<Func<R, object>> destinationMember, Expression<Func<E, object>> sourceMember)[] properties)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<E, R>().MapProperties(properties));
            return config.CreateMapper().Map<R>(@object);
        }

        internal static InteractionTypes GetTypeFromString(this string interactonType)
        {
            return interactonType switch
            {
                "1" or "" or "Default" or "default" => InteractionTypes.none,
                "gate" => InteractionTypes.gate,
                "postit" => InteractionTypes.postit,
                "roomeffect" => InteractionTypes.roomeffect,
                "dimmer" => InteractionTypes.dimmer,
                "trophy" => InteractionTypes.trophy,
                "bed" => InteractionTypes.bed,
                "scoreboard" => InteractionTypes.scoreboard,
                "vendingmachine" => InteractionTypes.vendingmachine,
                "alert" => InteractionTypes.alert,
                "onewaygate" => InteractionTypes.onewaygate,
                "loveshuffler" => InteractionTypes.loveshuffler,
                "habbowheel" => InteractionTypes.habbowheel,
                "dice" => InteractionTypes.dice,
                "bottle" => InteractionTypes.bottle,
                "teleport" => InteractionTypes.teleport,
                "rentals" => InteractionTypes.rentals,
                "pet" => InteractionTypes.pet,
                "pool" => InteractionTypes.pool,
                "roller" => InteractionTypes.roller,
                "fbgate" => InteractionTypes.fbgate,
                "iceskates" => InteractionTypes.iceskates,
                "normalskates" => InteractionTypes.normslaskates,
                "lowpool" => InteractionTypes.lowpool,
                "haloweenpool" => InteractionTypes.haloweenpool,
                "football" => InteractionTypes.football,
                "footballgoalgreen" => InteractionTypes.footballgoalgreen,
                "footballgoalyellow" => InteractionTypes.footballgoalyellow,
                "footballgoalred" => InteractionTypes.footballgoalred,
                "footballgoalblue" => InteractionTypes.footballgoalblue,
                "footballcountergreen" => InteractionTypes.footballcountergreen,
                "footballcounteryellow" => InteractionTypes.footballcounteryellow,
                "footballcounterblue" => InteractionTypes.footballcounterblue,
                "footballcountered" => InteractionTypes.footballcounterred,
                "banzaigateblue" => InteractionTypes.banzaigateblue,
                "banzaigatered" => InteractionTypes.banzaigatered,
                "banzaigateyellow" => InteractionTypes.banzaigateyellow,
                "banzaigategreen" => InteractionTypes.banzaigategreen,
                "banzaifloor" => InteractionTypes.banzaifloor,
                "banzaiscoreblue" => InteractionTypes.banzaiscoreblue,
                "banzaiscorered" => InteractionTypes.banzaiscorered,
                "banzaiscoreyellow" => InteractionTypes.banzaiscoreyellow,
                "banzaiscoregreen" => InteractionTypes.banzaiscoregreen,
                "banzaicounter" => InteractionTypes.banzaicounter,
                "banzaitele" => InteractionTypes.banzaitele,
                "banzaipuck" => InteractionTypes.banzaipuck,
                "banzaipyramid" => InteractionTypes.banzaipyramid,
                "freezetimer" => InteractionTypes.freezetimer,
                "freezeexit" => InteractionTypes.freezeexit,
                "freezeredcounter" => InteractionTypes.freezeredcounter,
                "freezebluecounter" => InteractionTypes.freezebluecounter,
                "freezeyellowcounter" => InteractionTypes.freezeyellowcounter,
                "freezegreencounter" => InteractionTypes.freezegreencounter,
                "freezeyellowgate" => InteractionTypes.freezeyellowgate,
                "freezeredgate" => InteractionTypes.freezeredgate,
                "freezegreengate" => InteractionTypes.freezegreengate,
                "freezebluegate" => InteractionTypes.freezebluegate,
                "freezetileblock" => InteractionTypes.freezetileblock,
                "freezetile" => InteractionTypes.freezetile,
                "jukebox" => InteractionTypes.jukebox,
                "musicdisc" => InteractionTypes.musicdisc,
                "triggertimer" => InteractionTypes.triggertimer,
                "triggerroomenter" => InteractionTypes.triggerroomenter,
                "triggergameend" => InteractionTypes.triggergameend,
                "triggergamestart" => InteractionTypes.triggergamestart,
                "triggerrepeater" => InteractionTypes.triggerrepeater,
                "triggeronusersay" => InteractionTypes.triggeronusersay,
                "triggerscoreachieved" => InteractionTypes.triggerscoreachieved,
                "triggerstatechanged" => InteractionTypes.triggerstatechanged,
                "triggerwalkonfurni" => InteractionTypes.triggerwalkonfurni,
                "triggerwalkofffurni" => InteractionTypes.triggerwalkofffurni,
                "triggercollision" => InteractionTypes.triggercollision,
                "triggerbotwalktouser" => InteractionTypes.triggerbotwalktouser,
                "triggerbotwalktofurni" => InteractionTypes.triggerbotwalktofurni,
                "actiongivescore" => InteractionTypes.actiongivescore,
                "actionposreset" => InteractionTypes.actionposreset,
                "actionmoverotate" => InteractionTypes.actionmoverotate,
                "actionresettimer" => InteractionTypes.actionresettimer,
                "actionshowmessage" => InteractionTypes.actionshowmessage,
                "actionkickuser" => InteractionTypes.actionkickuser,
                "actionteleportto" => InteractionTypes.actionteleportto,
                "wf_act_tiles" => InteractionTypes.actionusercollision,
                "wf_xtra_or_eval" => InteractionTypes.conditionleastoneconditiontrue,
                "wf_act_give_pointss" => InteractionTypes.actiongivescorenew,
                "wf_cstm_enable" => InteractionTypes.actiongiveenable,
                "wf_cstm_freeze" => InteractionTypes.actionfreeze,
                "actiontogglestate" => InteractionTypes.actiontogglestate,
                "actiongivereward" => InteractionTypes.actiongivereward,
                "actionfollowuser" => InteractionTypes.actionfollowuser,
                "actionmuteuser" => InteractionTypes.actionmuteuser,
                "actionbotchangelook" => InteractionTypes.actionbotchangelook,
                "actionbotwalktofurni" => InteractionTypes.actionbotwalktofurni,
                "actionbottalktoroom" => InteractionTypes.actionbottalktoroom,
                "actionbottalktouser" => InteractionTypes.actionbottalktouser,
                "actionbotfollowuser" => InteractionTypes.actionbotfollowuser,
                "actionbotgiveobject" => InteractionTypes.actionbotgiveobject,
                "actionbotteleport" => InteractionTypes.actionbotteleport,
                "actionexecutewiredgroup" => InteractionTypes.actionexecutewiredgroup,
                "actiongivepoints" => InteractionTypes.actiongivepoints,
                "actionfurnichangedirection" => InteractionTypes.actionfurnichangedirection,
                "actionrandomeffect" => InteractionTypes.actionrandomeffect,
                "conditionfurnishaveusers" => InteractionTypes.conditionfurnishaveusers,
                "conditionstatepos" => InteractionTypes.conditionstatepos,
                "conditionhasfurnionfurni" => InteractionTypes.conditionhasfurnionfurni,
                "conditionuserhasbadge" => InteractionTypes.conditionuserhasbadge,
                "conditionnothasfurnionfurni" => InteractionTypes.conditionnothasfurnionfurni,
                "conditionusernotonfurni" => InteractionTypes.conditionusernotonfurni,
                "conditionnegativestatepos" => InteractionTypes.conditionnegativestatepos,
                "conditionuserinteam" => InteractionTypes.conditionuserinteam,
                "conditionuserisnotinteam" => InteractionTypes.conditionuserisnotinteam,
                "conditiontimelessthan" => InteractionTypes.conditiontimelessthan,
                "conditiontimemorethan" => InteractionTypes.conditiontimemorethan,
                "conditiontriggeronfurni" => InteractionTypes.conditiontriggeronfurni,
                "conditiontriggernotonfurni" => InteractionTypes.conditiontriggernotonfurni,
                "conditiondatetime" => InteractionTypes.conditiondatetime,
                "conditionhowmanyusersinroom" => InteractionTypes.conditionhowmanyusersinroom,
                "conditionnegativehowmanyusers" => InteractionTypes.conditionnegativehowmanyusers,
                "conditionfurnitypematch" => InteractionTypes.conditionfurnitypematch,
                "conditionfurnitypedoesntmatch" => InteractionTypes.conditionfurnitypedoesntmatch,
                "conditionuserhasobject" => InteractionTypes.conditionuserhasobject,
                "arrowplate" => InteractionTypes.arrowplate,
                "preassureplate" => InteractionTypes.preassureplate,
                "ringplate" => InteractionTypes.ringplate,
                "colortile" => InteractionTypes.colortile,
                "colorwheel" => InteractionTypes.colorwheel,
                "floorswitch1" => InteractionTypes.floorswitch1,
                "floorswitch2" => InteractionTypes.floorswitch2,
                "firegate" => InteractionTypes.firegate,
                "glassfoor" => InteractionTypes.glassfoor,
                "specialrandom" => InteractionTypes.specialrandom,
                "specialunseen" => InteractionTypes.specialunseen,
                "wire" => InteractionTypes.wire,
                "wireCenter" => InteractionTypes.wireCenter,
                "wireCorner" => InteractionTypes.wireCorner,
                "wireSplitter" => InteractionTypes.wireSplitter,
                "wireStandard" => InteractionTypes.wireStandard,
                "puzzlebox" => InteractionTypes.puzzlebox,
                "levelupfloor" => InteractionTypes.levelupfloor,
                "snowboard" => InteractionTypes.snowboard,
                "bot" => InteractionTypes.bot,
                "mannequin" => InteractionTypes.mannequin,
                "groupClickable" => InteractionTypes.groupClickable,
                "groupGate" => InteractionTypes.groupGate,
                "impilamagic" => InteractionTypes.impilamagic,
                "recordsboard" => InteractionTypes.recordsboard,
                "actiongivescoretoteam" => InteractionTypes.actiongivescoretoteam,
                "actionjointeam" => InteractionTypes.actionjointeam,
                "actionremovefromteam" => InteractionTypes.actionremovefromteam,
                "wongamesrecordsboard" => InteractionTypes.wongamesrecordsboard,
                "lovelock" => InteractionTypes.lovelock,
                "conditionusernothasbadge" => InteractionTypes.conditionusernothasbadge,
                "cannon" => InteractionTypes.cannon,
                "conditionuserisingroup" => InteractionTypes.conditionuserisingroup,
                "conditionuserisnotingroup" => InteractionTypes.conditionuserisnotingroup,
                "conditionuserhaseffect" => InteractionTypes.conditionuserhaseffect,
                "conditionuserhasnoteffect" => InteractionTypes.conditionuserhasnoteffect,
                "actionchangedirection" => InteractionTypes.actionchangedirection,
                "triggerlongrepeater" => InteractionTypes.triggerlongrepeater,
                "actionfleeuser" => InteractionTypes.actionfleeuser,
                "actionfixroom" => InteractionTypes.actionfixroom,
                "snowboardjump" => InteractionTypes.snowboardjump,
                "redblob" => InteractionTypes.redblob,
                "greenblob" => InteractionTypes.greenblob,
                "photo" => InteractionTypes.photo,
                "crafting" => InteractionTypes.crafting,
                "seed" => InteractionTypes.seed,
                "rareseed" => InteractionTypes.rareseed,
                "crackable" => InteractionTypes.crackable,
                "votecount" => InteractionTypes.votecount,
                _ => InteractionTypes.none
            };
        }
    }
}