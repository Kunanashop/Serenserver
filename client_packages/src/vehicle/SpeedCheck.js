mp.events.add("sc:check",(maxSpeed) => {
  mp.events.callRemote("sc:check",global.localplayer.vehicle.getSpeed() * 3.6, maxSpeed);
});