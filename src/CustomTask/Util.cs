using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.MSBuild.CustomTask {
    class Util {

        public static ArrayList ConvertToArrayList(object[] objArray) {
            ArrayList result = new ArrayList();
            for(int i=0; i < objArray.Length; i++) {
                result.Add(objArray[i]);
            }
            return result;
        }

    }
}
