﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Combiner
{
	public static class ImportExportHandler
	{
		private static string m_XMLDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) , "..\\..\\XML"));

		public static void Import()
		{
			// Select file from dialog
			string filePath = string.Empty;
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = m_XMLDirectory;
				openFileDialog.Filter = "XML files (*.xml)|*.xml";
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					filePath = openFileDialog.FileName;
				}
			}

			if (string.IsNullOrEmpty(filePath))
			{
				return;
			}

			// Read XML and all that

			IEnumerable<CreatureData> importedCreatureData = CreatureXMLHandler.GetCreatureDataFromXML(filePath);

			// Save creatures to database

			List<Creature> creatures = new List<Creature>();
			foreach (var data in importedCreatureData)
			{
				creatures.Add(Database.GetCreature(data.left, data.right, data.bodyParts));
			}
			Database.SaveCreatures(creatures);
		}

		public static void Export()
		{
			// Select file to save as

			string filePath = string.Empty;
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = m_XMLDirectory;
				saveFileDialog.Filter = "XML files (*.xml)|*.xml";
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					filePath = saveFileDialog.FileName;
				}
			}

			if (string.IsNullOrEmpty(filePath))
			{
				return;
			}

			// Get creature data from database

			List<Creature> savedCreatures = Database.GetSavedCreatures();

			// Write to XML

			CreatureXMLHandler.AddCreaturesToXML(savedCreatures, filePath);

		}
	}
}