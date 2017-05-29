using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


using donet.io.rong.models;
using donet.io.rong.util;
using donet.io.rong.messages;
using Newtonsoft.Json;
using System.Text.Encodings.Web;

namespace donet.io.rong.methods {

    public class User{
   	
        private String appKey;
        private String appSecret;
        
		public User(String appKey, String appSecret) {
			this.appKey = appKey;
			this.appSecret = appSecret;
	
		}

        /**
	 	 * 获取 Token 方法 
	 	 * 
	 	 * @param  userId:用户 Id，最大长度 64 字节.是用户在 App 中的唯一标识码，必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户。（必传）
	 	 * @param  name:用户名称，最大长度 128 字节.用来在 Push 推送时显示用户的名称.用户名称，最大长度 128 字节.用来在 Push 推送时显示用户的名称。（必传）
	 	 * @param  portraitUri:用户头像 URI，最大长度 1024 字节.用来在 Push 推送时显示用户的头像。（必传）
		 *
	 	 * @return TokenReslut
	 	 **/
		public async Task<TokenReslut> getTokenAsync(String userId, String name, String portraitUri) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
			
			if(name == null) {
				throw new ArgumentNullException("Paramer 'name' is required");
			}
			
			if(portraitUri == null) {
				throw new ArgumentNullException("Paramer 'portraitUri' is required");
			}
			
	    	String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr += "name=" + HttpUtility.UrlEncode(name == null ? "" : name) + "&";
	    	postStr += "portraitUri=" + HttpUtility.UrlEncode(portraitUri == null ? "" : portraitUri) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));

            var rongClient = new RongHttpClient();
            string result = await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI + "/user/getToken.json", postStr, "application/x-www-form-urlencoded");
            return (TokenReslut)RongJsonUtil.JsonStringToObj<TokenReslut>(result);

        }

        /**
	 	 * 刷新用户信息方法 
	 	 * 
	 	 * @param  userId:用户 Id，最大长度 64 字节.是用户在 App 中的唯一标识码，必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户。（必传）
	 	 * @param  name:用户名称，最大长度 128 字节。用来在 Push 推送时，显示用户的名称，刷新用户名称后 5 分钟内生效。（可选，提供即刷新，不提供忽略）
	 	 * @param  portraitUri:用户头像 URI，最大长度 1024 字节。用来在 Push 推送时显示。（可选，提供即刷新，不提供忽略）
		 *
	 	 * @return CodeSuccessReslut
	 	 **/
        public async Task<CodeSuccessReslut> refreshAsync(String userId, String name, String portraitUri) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
            String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr += "name=" + HttpUtility.UrlEncode(name == null ? "" : name) + "&";
	    	postStr += "portraitUri=" + HttpUtility.UrlEncode(portraitUri == null ? "" : portraitUri) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (CodeSuccessReslut) RongJsonUtil.JsonStringToObj<CodeSuccessReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/refresh.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 检查用户在线状态 方法 
	 	 * 
	 	 * @param  userId:用户 Id，最大长度 64 字节。是用户在 App 中的唯一标识码，必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户。（必传）
		 *
	 	 * @return CheckOnlineReslut
	 	 **/
		public async Task<CheckOnlineReslut> checkOnlineAsync(String userId) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}

            

            String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (CheckOnlineReslut) RongJsonUtil.JsonStringToObj<CheckOnlineReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/checkOnline.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 封禁用户方法（每秒钟限 100 次） 
	 	 * 
	 	 * @param  userId:用户 Id。（必传）
	 	 * @param  minute:封禁时长,单位为分钟，最大值为43200分钟。（必传）
		 *
	 	 * @return CodeSuccessReslut
	 	 **/
		public async Task<CodeSuccessReslut> blockAsync(String userId, int minute) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
			
			if(minute ==0) {
				throw new ArgumentNullException("Paramer 'minute' is required");
			}
			
	    	String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
            postStr += "minute=" + HttpUtility.UrlEncode(Convert.ToString(minute) == null ? "" : Convert.ToString(minute)) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (CodeSuccessReslut) RongJsonUtil.JsonStringToObj<CodeSuccessReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/block.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 解除用户封禁方法（每秒钟限 100 次） 
	 	 * 
	 	 * @param  userId:用户 Id。（必传）
		 *
	 	 * @return CodeSuccessReslut
	 	 **/
		public async Task<CodeSuccessReslut> unBlockAsync(String userId) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
			
	    	String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (CodeSuccessReslut) RongJsonUtil.JsonStringToObj<CodeSuccessReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/unblock.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 获取被封禁用户方法（每秒钟限 100 次） 
	 	 * 
		 *
	 	 * @return QueryBlockUserReslut
	 	 **/
		public async Task<QueryBlockUserReslut> queryBlockAsync() {

	    	String postStr = "";
            var rongClient = new RongHttpClient();
            return (QueryBlockUserReslut) RongJsonUtil.JsonStringToObj<QueryBlockUserReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/block/query.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 添加用户到黑名单方法（每秒钟限 100 次） 
	 	 * 
	 	 * @param  userId:用户 Id。（必传）
	 	 * @param  blackUserId:被加到黑名单的用户Id。（必传）
		 *
	 	 * @return CodeSuccessReslut
	 	 **/
		public async Task<CodeSuccessReslut> addBlacklistAsync(String userId, String blackUserId) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
			
			if(blackUserId == null) {
				throw new ArgumentNullException("Paramer 'blackUserId' is required");
			}
			
	    	String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr += "blackUserId=" + HttpUtility.UrlEncode(blackUserId == null ? "" : blackUserId) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (CodeSuccessReslut) RongJsonUtil.JsonStringToObj<CodeSuccessReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/blacklist/add.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 获取某用户的黑名单列表方法（每秒钟限 100 次） 
	 	 * 
	 	 * @param  userId:用户 Id。（必传）
		 *
	 	 * @return QueryBlacklistUserReslut
	 	 **/
		public async Task<QueryBlacklistUserReslut> queryBlacklistAsync(String userId) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
			
	    	String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (QueryBlacklistUserReslut) RongJsonUtil.JsonStringToObj<QueryBlacklistUserReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/blacklist/query.json", postStr, "application/x-www-form-urlencoded" ));
		}
            
        /**
	 	 * 从黑名单中移除用户方法（每秒钟限 100 次） 
	 	 * 
	 	 * @param  userId:用户 Id。（必传）
	 	 * @param  blackUserId:被移除的用户Id。（必传）
		 *
	 	 * @return CodeSuccessReslut
	 	 **/
		public async Task<CodeSuccessReslut> removeBlacklistAsync(String userId, String blackUserId) {

			if(userId == null) {
				throw new ArgumentNullException("Paramer 'userId' is required");
			}
			
			if(blackUserId == null) {
				throw new ArgumentNullException("Paramer 'blackUserId' is required");
			}
			
	    	String postStr = "";
	    	postStr += "userId=" + HttpUtility.UrlEncode(userId == null ? "" : userId) + "&";
	    	postStr += "blackUserId=" + HttpUtility.UrlEncode(blackUserId == null ? "" : blackUserId) + "&";
	    	postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            var rongClient = new RongHttpClient();
            return (CodeSuccessReslut) RongJsonUtil.JsonStringToObj<CodeSuccessReslut>(await rongClient.ExecutePostAsync(appKey, appSecret, RongCloud.RONGCLOUDURI+"/user/blacklist/remove.json", postStr, "application/x-www-form-urlencoded" ));
		}
    }
       
}