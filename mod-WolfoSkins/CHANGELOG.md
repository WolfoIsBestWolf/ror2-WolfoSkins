## Changelog
```
v3.0.1
Skins now sorted after after DotFlare/SurvivorReturns skins.
Removed uneeded debug logging.
Adjusted Bandit Black/Purple/Yellow to better match RoRR colors.
Adjusted CHEF Green/Orange to be less green.
Fixed a MulT skin name being wrong.
Fixed Lem Acrid spines not disappearing in lobby.
Readded sort config just in case.
```
```
V3.0.0
Added 3 Operator Skins
Added 4 Drifter Skins
Added 1 Seeker Skin
Added 1 Void Fiend recolor.
Remade 2 Chirr skins for new model
Remade 1 CHEF skin to use Mastery model.
Added False Son Provi alt with Cyan highlights.
Adjusted MulT skins.
Adjusted Engi Blue Pink.
Adjusted False Son Gold.
Desaturated Void Fiend Blue.


Skins are now created when you select them for the first time, which should speed up load times for lower end machines.
Skins now sorted after after SS2 skins naturally.
Removed sort config.


Added Russian translation thanks to 'coldeib' on Discord.

Fixed False Son Gold flickering in the lobby.
Fixed Void Fiend Orange hand looking improper.
Fixed modded skin glows misaligning at lower texture scales.
Fixed some skins being less shiny than inteded.
Removed Pilot skins.
```
```
v2.2.10
Removed SS2 skins, due to charcacters getting a remodel.
-Ill get to re-doing the skins sometime after AC.

v2.2.9
Fixed mod skins sometimes not applying due to the internal changes.
Fixed Engi Turrets not being skinned for the mods skins.

v2.2.8
Forgot to fix code for some modded characters, making part of mod support not load.

v2.2.7
Fixed mod no longer working after R2API updates.
Resorted Acrid Skins


v2.2.6
Fixed Startup Softlock with Unlockify.

v2.2.5
Added more nullref checks for, I guess empty SurvivorDefs?
Only checks skins added by mod.

v2.2.4
Added check for if False Son skill is actually used by False Son for a Paladin skill mod.

v2.2.3
Fixed Unlock-All config not unlocking some skins if changed in game.
Fixed Huntress ears/antenna not showing up.
Fixed Achievement description not updating when switching profiles.
Unlock assinging is skipped if Goorakh.ModelSwapSkin to prevent indexOutOfBounds and checking 13k skins.

 
v2.2.2
Fixed potential nullRef on start up on too big modpacks


v2.2.1
Fixed Lunar Stakes small spikes not matching False Sons spikes.
Fixed Artiificer Orange looking darker than intended.
Fixed ArsonistGM Blue copying wrong skin and not having icon.
Fixed a Chirr skin using wrong icon


v2.2.0 
Updated for SotSf 3 & 4
-Fixed Chef Skins for Oven
-Updated Chef skins for Ice Box
-Updated False Son skins for new M2

Generally optimized mod, before MemOp hit.
Bit hard to tell how well it works out with the new additions.
Mod now tagged as Client Side.
Unlocks now includes Eclipse 4 (or above) as possible unlock spot.
Tier 2 unlock now just needs 2 of 4, not specifically AltBoss & AltMode
Colossus Recolors now need you to own DLC2.
Recolored wine red engi to be normal Red. (Weird returns palette)
Removed ChefMod skins.
Fixed Unlock-All config not unlocking some skins.
 


v2.1.2
Quick fix for mod not loading with mods that add Engi Turrets.

v2.1.1
Redid Han-D returns skins as the new model they added needed new textures.
Fixed Han-D Default drones wrong texture format.
Crystals in False Sons attacks are now colored (automatically)
Fixed issue with FalseSon:Provi spikes.
Fixed Bandit achievement not migrating


v2.1.0
Added Seeker : Default : Black/Orange
Colored Braclet and cleaned up FalseSon:Provi
Made Ne Colossus Engi Red slightly less strong.
Adjusted Harpoons on Engi skins.
Made Green Colossus Acrid more green and need 2nd Achievement.
Made Purple Colossus Arti have more emission like the white one.


v2.0.4
Added more saftey checks because mod wouldn't load with incomplete/unnamed SurvivorDefs.
Fixed mod not loading when downloaded manually.

v2.0.3
Fixed issue with Acrids spikes being missing on some skins in lobby.
First-time achievements will no longer give notifications.

v2.0.2
Fixed Captain not migrating anymore.
Fixed Paladin skin not being emssive anymore.
Fixed Pilot and NemForcer not migrating. 

v2.0.1
Now uses achievements for trackers since those stay even if a mod gets uninstalled.
Fixed issue with transffering old achievements leading to always getting unlocks for some of the characters. 
(Should automatically revoke the the achievements it should not have granted, alternatively just re-lock, auto-unlock)
(Bandit, Engi, HanD, Sniper, Rocket, Arsonist, Paladin, Ravager were affected)
Fixed one Commando skin not being unlockable
Fixed one Merc skin not being unlockable
Fixed Executioner skins not migrating.


v2.0.0 : Real Sots update.
New Colossus reskins will need you to do both; Beat Wave 50 and Defeat and one of the Alt Bosses. 

A lot of technical achievement stuff, achievements are reset.
An automatic unlock checker should run the first time so not much should be lost.

Mod now uses AssetBundle for optimization.
Mow now uses LanguageFiles for translation and organisation.

Added False Son : Gold
Added False Son : Mastery Providence
Added Commando : Colossus Blue
Added Huntress : Colossus Red
Added Bandit : Colossus Orange
Added Engi : Colossus Black/Red
Added Mul-T : Colossus Purple
Added Artificer : Colossus Purple (Replacing previous Purple skin)
Added Artificer : Colossus Gray/Blue
Redid Merc : Colossus Green
Added REX : Colossus Orange
Added Loader : Colossus Yellow
Added Acrid : Colossus Purple
Added Acrid : Colossus Green-er
Added Captain : Colossus Red/Black (Replacing previous Red skin)
Added Railgunner : Colossus Purple

Added Enforcer : Bot Red
Added Rocket : Mastery Red

Made Mul-T skins have missive parts. (despite saner judgement)
Changed Rex lava flowers to petals instead of daisies.
Changed Miner Orange to Blacksmith model.
Fixed Paladin to use updated texture.
Fixed some issues on the Chef textures.


v1.9.15 - Textures added by this mod can now scale down as they should. (Optimization)
v1.9.14 - Fixed unlock all issue with starting at Desolator Skins
v1.9.13 - Fixed unlock all issue with starting at Chef_DLC2 Skins
v1.9.12
Fixed skins from this mod unlocking from Rebirth ending.
Fixed a bug related to StarStorm2 Executioner VFX not working.
Fixed a bug with RedAlert2 replacing TeslaTrooper mod.
Added Merc Colossus variant : White/Neon Green 
Added basic Chef Recolors (Red/Yellow,Green/Orange,Blue/Pink)(Will probably be changed later on)

v1.9.11
Fixed a error during startup if the unlock all config was enabled.

v1.9.10
Fixes infinite loading with CHEF_MOD and Paladin being bugged.

v1.9.9 - Sots Fix
Skins for Sots characters and variants of Sots skins will come at a later time. 
Removed Prismatic Trial/Dissonant Achievements.
Fixed bug with Unlock All config causing issues on with modded survivor skins.

v1.6.2
Fixed a bug where attacks with visual overlays such as Mercs could break modded code.
Added Executioner : Blue/Green
Added Nemesis Enfrocer : Big Yellow

v1.6.1
Fixed Arsonist, Tesla, Desolator skins not unlocking.

v1.6.0
Added Arsonist : Orange
Added Arsonist : Blue
Added Arsonist : Blue GM
Added Tesla Trooper Colors : White, Strong Blue, Neon Blue, Neon Cyan, Hot Pink, Neon Red
Added Desolator Colors : Gray, Strong Green, Neon Green, Safety Yellow, Neon Yellow, Neon Orange
Added Paladin : Gold/Green

Hid placeholder prism achievements.
Gave CHEF colored cleavers.
Fixed CHEF alt primary arm color not changing with skins.
Made Ravager skin look less weird.

v1.5.0
Added Han-D : Green/Yellow
Added Han-D : Green/Yellow GM
Added Acrid : Black/Green
Added CHEF : Blue
Added CHEF : Red/Cyan
Added Sniper : Gray
Added Artificer : Purple/Yellow
Added Rocket : Red
Added Rocket : Blue
Added Ravager : Red/Black
Added Pilot : Blue/Yellow
Added Pilot : White/Black/Red
Added Pilot : Red/Purple/Cyan
Added Void Fiend : Orange
Added Enforcer : Yellow
Added Miner : Black/Orange
Added Miner : Orange
Added Miner : Emerald
Added Chirr : Pink/White/Green
Added Chirr : Orange/Yellow

Added another achievement type (Beat run with Dissonance or beat Prismatic run)
Skins are now sorted later than Grand Mastery and other skin mods.

v1.4.0
Added Han-D : Orange/Black
Added Han-D : Orange/Black GM
Added Executioner : Mastery Yellow
Added CHEF : Green
Added CHEF : Cyan/Pink/White
Added Bandit : Yellow Hat/Lime
Added Loader : Red
Added Bandit : Blue/Yellow
Added Sniper : Orange

Achievements for modded characters are now hidden if the mod is not installed.

Executioner Skins now have colored smoke trails and overlay mat
-Can't really do much more with R2API.SkinsVFX at the moment

Commando (Provi Trial) Guns are now silverish color
CHEF (Black) Now has white facial features

v1.3.0
Added Railgunner : Green/Brown
Added Executioner : Black/Red
Engi Skins : Added more Light patterns and better Blue Harpoons

Loader (Green) now uses default skin mech.
Fixed Enforcer Skin not unlocking

v1.2.0
Made it it's own mod instead of being part of SimulacrumAdditions.
Skins can now be unlocked by beating Twisted Scavengers too.

Updated various skins
Added 5 more Skins
-Added Huntress : Bee
-Added Acrid : Lemurian
-Added REX : Red/Yellow/Black
-Added Engineer : Blue/Pink
-Added Enforcer : Red/Blue/Yellow


Added config for no unlock requirement
Added config to nerf Voidlings stats (not changed by default) or limit his level to 99 for level cap raising mods (on by default)

v1.1.1 - (Simulacrum Additions)
-Added Commando : Provi
-Added Commando : Black Hat/Purple

v1.1.0 - (Simulacrum Additions update)
-Captain Red/Black
-Mul-T : Damage
-Mul-T : Healing
-Artificer : Orange/Black
-Artificer : Rainbow
-Huntress : Black/Pink
-Acrid : White/Blue
-CHEF : Red
-CHEF : Black
-Han-D : Gold
-Han-D : Gold GM
-Loader : (Green)
-Mercenary : (Blue/Gray)
-Mercenary : (Red/Gray)
-Void Fiend : (Black/Red
-Void Fiend : (White/Blue)
-Railgunner : (White/Black/Blue)

v1.0.5 (LittleGameplayTweaks)
-Bandit (Red Hat/Light Brown)

v1.0.0 (LittleGameplayTweaks)
-Commando : (Unused SotV skin)
-REX : Blue
-Captain : Pink

