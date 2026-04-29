
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;
using EmptyFlow.SciterAPI.Client.DeveloperConsole;
using EmptyFlow.SciterAPI.Client.Models;
using EmptyFlow.SciterAPI.Client.PseudoSom;
using EmptyFlow.SciterAPI.Enums;
using System.Numerics;

// These example developer used for experiment with some new features
// please use another examples in folder Examples. Thanks!

namespace GaiaiLogic {

	public static class Program {

		[STAThread]
		public static void Main () {

			//var pathToSciter = "C:/IDEs/sciter/sciter-js-sdk-6.0.3.9";
			var pathToSciter = "C:/IDEs/sciter/sciter-js-sdk-6.0.3.15";

			var host = new SciterAPIHost ( Path.Combine ( pathToSciter, "bin/windows/x64" ), true, true );
			var path = "file://" + Path.Combine ( pathToSciter, "samples/html/details-summary.htm" );
			//var path = "file://" + Path.Combine ( pathToSciter, "samples/canvas/particles.htm" );
			//host.Callbacks.AddAttachBehaviourFactory ( "testbehaviour", ( element ) => new TestGraphicsEventHandler ( element, host ) );
			//host.Callbacks.AddAttachBehaviourFactory ( "psom", ( element ) => new PseudoSomHandler ( element, host ) );
			//host.EnableDebugMode ();
			//host.EnableFeatures ();
			host.CreateWindow ( asMain: true, debugOutput: true );
			host.AddWindowEventHandler ( new MyWindowEventHandler ( host.MainWindow, host ) );
			host.LoadFile ( path );
			host.Process ();
		}
	}

	public class MyWindowEventHandler : SciterEventHandler {

		DeveloperConsole _devConsole;

		public MyWindowEventHandler ( nint window, SciterAPIHost host ) : base ( window, host ) {
			_devConsole = new DeveloperConsole ( host, host.MainWindow );
		}

		public override void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) {
			if ( cmd == BehaviourEvents.DOCUMENT_READY ) {

				if ( Host.CloseWindow ( Host.MainWindow, "10" ) ) {

				}
			}
		}

	}

	public class TestModel : IPseudoSomModel {

		private readonly SciterAPIHost m_host;

		private string _unique = "";

		public TestModel ( SciterAPIHost host ) {
			m_host = host;
		}

		public SciterValue CallMethod ( string name, IEnumerable<SciterValue> parameters ) {
			return m_host.CreateNullValue ();
		}

		public HashSet<string> GetMethods () {
			return [];
		}

		public string GetModelName () => "mymodel";

		public HashSet<string> GetProperties () => ["counter"];

		public int Counter { get; set; } = 5;

		public string Unique => _unique;

		public SciterValue GetPropetyValue ( string name ) {
			return name switch {
				"counter" => m_host.CreateValue ( Counter ),
				_ => m_host.CreateNullValue ()
			};
		}

		public bool SetPropetyValue ( SciterValue value, string name ) {
			if ( name == "counter" ) {
				Counter = m_host.GetValueInt32 ( ref value );
				return true;
			}

			return false;
		}

		public void SetUnique ( string unique ) {
			_unique = unique;
		}
	}

	public class PseudoSomHandler : ElementEventHandler {

		readonly TestModel _model;

		public PseudoSomHandler ( nint element, SciterAPIHost host ) : base ( element, host ) {
			_model = new TestModel ( host );
		}

		public override void AfterRegisterEvent () {
			PseudoSom.RegisterModel ( _model, Host, Host.MainWindow, m_subscribedElement );
		}

		public override (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) {
			return PseudoSom.Handle ( _model, Host, name, arguments );
		}

	}

	public class TestGraphicsEventHandler : ElementEventHandler {

		public int particleSize = 4;

		public int maxParticles = 40;

		public int threshold = 100;

		public int width = 0;

		public int height = 0;

		public List<(int x, int y, int vx, int vy)> particles = new List<(int x, int y, int vx, int vy)> ();

		public TestGraphicsEventHandler ( nint element, SciterAPIHost host ) : base ( element, host ) {
			Color1 = Host.GraphicsGetColor ( 0, 204, 0 );
			Color2 = Host.GraphicsGetColor ( 0, 0, 255 );
			Color3 = Host.GraphicsGetColor ( 255, 255, 0 );

			Random random = new Random ( (int) ( DateTime.UtcNow - new DateTime ( 1970, 1, 1 ) ).TotalSeconds );

			/*
			 for (let i = 0; i < maxParticles; i++) {
	  let particle = {
		x: Math.random() * canvas.width,
		y: Math.random() * canvas.height,
		vx: Math.random(),
		vy: Math.random()
	  }
	  particles.push(particle);
	}
			 */
			width = Convert.ToInt32 ( Host.GetElementStyleProperty ( m_subscribedElement, "width" ).Replace ( "px", "" ) );
			height = Convert.ToInt32 ( Host.GetElementStyleProperty ( m_subscribedElement, "height" ).Replace ( "px", "" ) );
			for ( int i = 0; i < maxParticles; i++ ) {
				var randomWidth = (double) random.Next ( 100 ) / (double) 100;
				var randomHeight = (double) random.Next ( 100 ) / (double) 100;
				var randomVerticalX = random.Next ( 100 );
				var randomVerticalY = random.Next ( 100 );
				particles.Add ( ((int) ( randomWidth * width ), (int) ( randomHeight * height ), randomVerticalX, randomVerticalY) );
			}
		}

		public uint Color1 { get; set; }

		public uint Color2 { get; set; }

		public uint Color3 { get; set; }

		private void DrawLine ( nint gfx, SciterPoint particle, SciterPoint particle2 ) {
			var path = Host.GraphicsPath (
				( path ) => {
					Host.GraphicsPathMoveTo ( path, particle, false );
					Host.GraphicsPathLineTo ( path, particle2, false );
				}
			);
			Host.GraphicsDrawPath ( gfx, path, DrawPathMode.DrawFillAndStroke );
		}

		public override void DrawEvent ( DrawEvents command, nint gfx, SciterRectangle area, uint reserved ) {
			if ( command == DrawEvents.DRAW_CONTENT ) {
				Host.GraphicsFillMode ( gfx, false );

				Host.GraphicsSaveState ( gfx );

				/*Host.GraphicsFillColor ( gfx, Host.GraphicsGetColor ( 255, 255, 255 ) );
				Host.GraphicsDrawRectangle ( gfx, area.Left, area.Top, area.Left + area.Width, area.Top + area.Height );*/

				for ( var i = 0; i < maxParticles; i++ ) {
					var particle = particles[i];
					//context.fillRect ( particle.x - particleSize / 2, particle.y - particleSize / 2, particleSize, particleSize );
					Host.GraphicsDrawRectangle ( gfx, area.Left + ( particle.x - particleSize / 2 ), area.Top + ( particle.y - particleSize / 2 ), area.Left + particleSize, area.Top + particleSize );

					for ( var j = 0; j < maxParticles; j++ ) {
						if ( i != j ) {
							var particle2 = particles[j];
							var distanceX = Math.Abs ( particle.x - particle2.x );
							var distanceY = Math.Abs ( particle.y - particle2.y );
							if ( distanceX < threshold && distanceY < threshold ) {
								//context.lineWidth = ( ( threshold * 2 ) - ( distanceX + distanceY ) ) / 50;
								Host.GraphicsSetLineWidth ( gfx, ( ( threshold * 2 ) - ( distanceX + distanceY ) ) / 50 );
								var color = 200 - Math.Floor ( (double) ( distanceX + distanceY ) );
								//context.strokeStyle = 'rgb(' + color + ',' + color + ',' + color + ')';
								Host.GraphicsLineColor ( gfx, Host.GraphicsGetColor ( (uint) color, (uint) color, (uint) color ) );
								DrawLine ( gfx, new SciterPoint ( particle.x, particle.y ), new SciterPoint ( particle2.x, particle2.y ) );
							}
						}
					}
					particle.x = particle.x + particle.vx;
					particle.y = particle.y + particle.vy;
					if ( particle.x > width - particleSize || particle.x < particleSize )
						particle.vx = -particle.vx;
					if ( particle.y > height - particleSize || particle.y < particleSize )
						particle.vy = -particle.vy;
				}

				Host.GraphicsRestoreState ( gfx );
			}
		}

		/*
	function animate() {
	  context.clearRect(0, 0, canvas.width, canvas.height);
	  for (let i = 0; i < maxParticles; i++) {
		let particle = particles[i];
		context.fillRect(particle.x - particleSize / 2, particle.y - particleSize / 2, particleSize, particleSize);
		for (let j = 0; j < maxParticles; j++) {
		  if (i != j) {
			let particle2 = particles[j];
			let distanceX = Math.abs(particle.x - particle2.x);
			let distanceY = Math.abs(particle.y - particle2.y);
			if (distanceX < threshold && distanceY < threshold) {
			  context.lineWidth = ((threshold * 2) - (distanceX + distanceY)) / 50;
			  let color = 200 - Math.floor(distanceX + distanceY);
			  context.strokeStyle = 'rgb(' + color + ',' + color + ',' + color + ')';
			  line(particle, particle2);
			}
		  }
		}
		particle.x = particle.x + particle.vx;
		particle.y = particle.y + particle.vy;
		if (particle.x > canvas.width - particleSize || particle.x < particleSize)
		  particle.vx = -particle.vx;
		if (particle.y > canvas.height - particleSize || particle.y < particleSize)
		  particle.vy = -particle.vy;
	  }
	  requestAnimationFrame(animate);
	}

		let canvas = document.getElementById('myCanvas');
	let context = canvas.getContext('2d');
	let particles = [];
	let particleSize = 4;
	let maxParticles = 40;
	let threshold = 100;
	for (let i = 0; i < maxParticles; i++) {
	  let particle = {
		x: Math.random() * canvas.width,
		y: Math.random() * canvas.height,
		vx: Math.random(),
		vy: Math.random()
	  }
	  particles.push(particle);
	}
	context.fillStyle = 'white';
	animate();
		 */

	}

}


