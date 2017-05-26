//index.js
//获取应用实例
var app = getApp()
Page({
  data: {
    motto: 'Hello World test!',
    userInfo: {},
    testData:{}
  },
  //事件处理函数
  bindViewTap: function() {
    wx.navigateTo({
      url: '../logs/logs'
    })
  },
  onLoad: function () {
    console.log('onLoad')


    var that = this

//登录测试

wx.login({
  success:function(res){
    var code=res.code;
    wx.request({
      url: 'http://localhost:63276/api/values/Login?code='+code,
      success: function (res) {
        that.setData({
          testData: res.data
        })
        //console.log(data);
      }
    })
  }
})


//webapi请求
/*
wx.request({
  url: 'http://localhost:63276/api/values?id=1',
  success:function(res){
    that.setData({
      testData:res.data
    })
    //console.log(data);
  }
})
*/


    
    //调用应用实例的方法获取全局数据
    app.getUserInfo(function(userInfo){
      //更新数据
      that.setData({
        userInfo:userInfo
      })
    })
  }
})
