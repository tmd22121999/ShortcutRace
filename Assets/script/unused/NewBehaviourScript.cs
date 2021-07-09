/* if(Vector3.Distance(thisbody.transform.position,map.path.GetPoint(i)) < 4)
                        startpoint=i;
                    v2=map.path.GetPoint(i+1) - map.path.GetPoint(i);
                    a=-v1.z;b=v1.x;a1=-v2.z;b1=v2.x;
                    c=-a*thisbody.transform.position.x-b*thisbody.transform.position.z;
                    c1=-a*map.path.GetPoint(i).x-b*map.path.GetPoint(i).z;
                    z=(-c1*a+c*a1)/(a*b1-a1*b);
                    x=(-c-b*z)/a;
                    Vector3 giaodiem = new Vector3(x,thisbody.transform.position.y,z);
                    
                   // Debug.DrawLine(thisbody.transform.position,giaodiem);
                    
                    //float distance = UnityEditor.HandleUtility.DistancePointLine(map.path.GetPoint(i),thisbody.transform.position,goal.transform.position);
                    */