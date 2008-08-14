/*
NeatUpload - an HttpModule and User Control for uploading large files
Copyright (C) 2008  Dean Brettle

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Web;
using System.Collections.Specialized;

namespace Brettle.Web.NeatUpload
{
	public class UploadModule
	{		
		public static string PostBackIDQueryParam {
			get { return InstalledModule.PostBackIDQueryParam; }
		}

		public static string FileFieldNamePrefix {
			get { return InstalledModule.FileFieldNamePrefix; }
		}

		public static string ConfigFieldNamePrefix {
			get { return InstalledModule.ConfigFieldNamePrefix; }
		}

		public static bool IsEnabled {
			get { return (InstalledModule != null && InstalledModule.IsEnabled); }
		}

		public static NameValueCollection Unprotect(string armoredString)
		{
			return InstalledModule.Unprotect(armoredString);
		}

		public static string Protect(NameValueCollection nvc)
		{
			return InstalledModule.Protect(nvc);
		}

		public static IUploadedFileCollection Files {
			get { return InstalledModule.Files; }
		}

		public static string PostBackID {
			get { return InstalledModule.PostBackID; }
		}

		public static void SetProcessingState(string postBackID, string controlID, object state)
		{
			InstalledModule.SetProcessingState(postBackID, controlID, state);
		}

		public static void BindProgressState(string postBackID, string controlID, IUploadProgressState progressState)
		{
			InstalledModule.BindProgressState(postBackID, controlID, progressState);
		}

		public static void CancelPostBack(string postBackID)
		{
			InstalledModule.CancelPostBack(postBackID);
		}
		
		private static bool _IsInstalled = true;
		private static IUploadModule _InstalledModule;
		private static IUploadModule InstalledModule {
			get {
				if (!_IsInstalled) 
					return null;
				if (_InstalledModule == null)
				{
					foreach (IHttpModule module in HttpContext.Current.ApplicationInstance.Modules)
					{
						if (module is IUploadModule)
						{
							_InstalledModule = (IUploadModule) module;
							break;
						}
					}
					if (_InstalledModule == null) 
						_IsInstalled = false;
				}
				return _InstalledModule;
			}
		}
	}
}
