using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gipper
{
	internal class WindowFrameTiler
	{
		// fields
		private Screen _screen;
		private int _rowCount;
		private int _columnCount;


		// constructors
		public WindowFrameTiler(Screen screen, int rowCount, int columnCount)
		{
			Debug.Assert(screen != null);
			Debug.Assert(rowCount >= 1);
			Debug.Assert(columnCount >= 1);

			_screen = screen;
			_rowCount = rowCount;
			_columnCount = columnCount;
		}


		// methods
		public void Tile(IList<GipperWindowFrame> frames)
		{
			GipperWindowFrame[,] frameArray = SortTilesAccordingToCurrentPositions(frames);

			// HACKHACK: Need a better way to figure out the height of a tool window's caption
			int toolWindowCaptionHeight = 9;

			// NOTE: Be sure to adjust for screen DPI.
			float scalingFactor = Helper.GetScreenDpiScalingFactor();
			int cx = (int) (((float) _screen.WorkingArea.Width / (float) scalingFactor) / ((float) _columnCount));
			int cy = (int) (((float) _screen.WorkingArea.Height / (float) scalingFactor) / ((float) _rowCount));
			int y = (int) (((float) _screen.WorkingArea.Top) / ((float) scalingFactor));
			
			for(int row = 0; row < _rowCount; ++row)
			{
				int x = (int) (((float) _screen.WorkingArea.Left) / ((float) scalingFactor));
				for(int column = 0; column < _columnCount; ++column)
				{
					GipperWindowFrame frame = frameArray[row, column];
					frame.X = x;
					frame.Y = y;
					frame.CX = cx;
					frame.CY = cy - (int) ((float) toolWindowCaptionHeight * (float) scalingFactor);
					frame.InvokeSetFramePos();

					x += cx;
				}

				y += cy;
			}
		}


		// private methods
		private GipperWindowFrame[,] SortTilesAccordingToCurrentPositions(IList<GipperWindowFrame> frames)
		{
			GipperWindowFrame[,] frameArray = new GipperWindowFrame[_rowCount, _columnCount];

			// ensure we have the correct initial positions
			foreach(GipperWindowFrame frame in frames)
				frame.InvokeGetFramePos();

			// first, ogranize the frame windows down by column
			frames = frames.OrderBy(f => f.X).ToList();
			int i = 0;
			for(int column = 0; column < _columnCount; ++column)
			{
				for(int row = 0; row < _rowCount; ++row)
				{
					frameArray[row, column] = frames[i];
					++i;
				}
			}

			// then, organize each column of frame windows by row
			for(int column = 0; column < _columnCount; ++column)
			{
				IList<GipperWindowFrame> columnFrames = new GipperWindowFrame[_rowCount];
				for(int row = 0; row < _rowCount; ++row)
					columnFrames[row] = frameArray[row, column];

				columnFrames = columnFrames.OrderBy(f => f.Y).ToList();

				for(int row = 0; row < _rowCount; ++row)
					frameArray[row, column] = columnFrames[row];
			}

			return frameArray;
		}
	}
}

