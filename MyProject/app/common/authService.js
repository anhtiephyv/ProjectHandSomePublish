define(['app'], function (app) {
    'use strict';
    var app = angular.module('MyApp', []);
	app.factory('authService', authService);
	authService.$inject = ['$http'];

	function authService($http) {
		return {
			//constructor(private http: HttpClient) { }
			 rootUrl: 'http://localhost:1624',


			 userAuthentication: function userAuthentication(username, password) {
			 	debugger;
			var data = "username=" + username + "&password=" + password + "&grant_type=password";
			return $http.post('/token', data, {
				headers:
                   { 'Content-Type': 'application/x-www-form-urlencoded', }
			});
		},
			 registerUser: function registerUser(user) {
			User = {
				UserName: user.UserName,
				Password: user.Password,
				Email: user.Email,
				FirstName: user.FirstName,
				LastName: user.LastName
			}
			//var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
			//return this.http.post(this.rootUrl + '/api/User/Register', body, { headers: reqHeader });
		},

		//function userAuthentication($scope.loginData.userName, $scope.loginData.password) {
		//    var data = "username=" + userName + "&password=" + password + "&grant_type=password";
		//    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True' });
		//    return this.http.post(this.rootUrl + '/token', data, { headers: reqHeader });
		//}

			 getUserClaims:	function getUserClaims() {
			     return this.http.get(this.rootUrl + '/api/GetUserClaims');
			 },
		    logout:function(){

		}

	}
}
})
